using System;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Client.Services.PersonAssignmentServicesFolder;
using Festivalcito.Client.Services.ShiftServicesFolder;
using Festivalcito.Client.Services.ShiftAssignmentServicesFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Blazored.LocalStorage;

namespace Festivalcito.Client.Pages{

	partial class VolunteerPage{

        [Inject]
        public IPersonService? PersonService { get; set; }
        [Inject]
        public IShiftService? ShiftService { get; set; }
        [Inject]
        public IShiftAssignmentService? ShiftAssignmentService { get; set; }
        [Inject]
        public IPersonAssignmentService? PersonAssignmentService { get; set; }


        List<Person> listOfAllPeople = new List<Person>();
        List<Shift> listOfAllShifts = new List<Shift>();
        List<ShiftAssignment> listOfShiftAssignments = new List<ShiftAssignment>();

        List<Shift> ListOfPersonAreaShifts = new List<Shift>();

        public string loggedInUserEmail = "";
        int personAssignmentId = 0;
        

        private Person PersonValidation = new Person();
        private EditContext? EditContext;

        public VolunteerPage(){
		}




        private void HandleValidSubmit()
        {
            PersonService!.UpdatePerson(PersonValidation);
        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }

        protected override void OnInitialized(){
            base.OnInitialized();
            EditContext = new EditContext(PersonValidation);
        }



        protected override async Task OnInitializedAsync(){

            loggedInUserEmail = await localStore.GetItemAsync<string>("userLoggedInEmail");
            PersonValidation = await PersonService!.ReadPersonEmail(loggedInUserEmail);

            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            listOfShiftAssignments = (await ShiftAssignmentService!.ReadAllShiftAssignments())!.ToList();
            Console.WriteLine("listOfAllShifts count: " + listOfAllShifts.Count());
            Console.WriteLine("PersonValidation areaName: " + PersonValidation.areaName);

            personAssignmentId = (await PersonAssignmentService!.ReadPersonAssignment(PersonValidation.PersonID)).PersonAssignmentId;
            Console.WriteLine("personAssignmentId " + personAssignmentId);
            updateShiftsTable();

        }


        public async void TakeShiftClicked(Shift shift){
            Console.WriteLine("TakeShiftClicked");
            ShiftAssignment newShiftAssigned = new ShiftAssignment();
            int personid = PersonValidation.PersonID;

            
            newShiftAssigned.personassignmentid = personAssignmentId;
            newShiftAssigned.ShiftId = shift.ShiftID;

            await ShiftAssignmentService!.CreateShiftAssignment(newShiftAssigned);

            await updateLists();
            updateShiftsTable();
        }

        public async void removeShiftClicked(int shiftId){
            int ShiftAssignmentidTmp = findShiftAssignmentID(shiftId);
            await ShiftAssignmentService!.DeleteShiftAssignment(ShiftAssignmentidTmp);
            await updateLists();
            updateShiftsTable();
        }

        public void updateShiftsTable(){
            Console.WriteLine("updateShiftsTable");
            
            ListOfPersonAreaShifts.Clear();
            foreach (Shift shift in listOfAllShifts){

                if (shift.areaName == PersonValidation.areaName){
                    foreach (ShiftAssignment shiftAssignment in listOfShiftAssignments){
                        if (shiftAssignment.ShiftId == shift.ShiftID){
                            shift.backgroundColor = "red";
                            
                        }
                    }
                    Console.WriteLine("color: "+shift.backgroundColor);
                    if (shift.IsLocked == true)
                    {
                        shift.backgroundColor = "grey";
                    }
                    ListOfPersonAreaShifts.Add(shift);

                }
            }
            
            Console.WriteLine("ListOfPersonAreaShifts count: " + ListOfPersonAreaShifts.Count());
            StateHasChanged();
        }
        public async Task updateLists()
        {
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            listOfShiftAssignments = (await ShiftAssignmentService!.ReadAllShiftAssignments())!.ToList();
        }

        public int findShiftAssignmentID(int shiftId)
        {
            foreach (ShiftAssignment shiftAssignment in listOfShiftAssignments)
            {
                if (shiftAssignment.ShiftId == shiftId && shiftAssignment.personassignmentid == personAssignmentId)
                {
                    return shiftAssignment.ShiftAssignmentid;
                }
            }
            return -1;
        }

    }

    







}

