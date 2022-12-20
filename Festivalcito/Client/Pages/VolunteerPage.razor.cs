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

    partial class VolunteerPage {

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

        public string? loggedInUserEmail { get; set; }
        int personAssignmentId = 0;
        float personTotalPoints = 0;


        private Person PersonValidation = new Person();
        private EditContext? EditContext;

        List<Shift> personShiftsList = new List<Shift>();

        public VolunteerPage() {
        }




        private void HandleValidSubmit()
        {
            PersonService!.UpdatePerson(PersonValidation);
        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }

        protected override void OnInitialized() {
            base.OnInitialized();
            EditContext = new EditContext(PersonValidation);
        }



        protected override async Task OnInitializedAsync() {
            try{
                loggedInUserEmail = await localStore.GetItemAsync<string>("userLoggedInEmail");
                Console.WriteLine("\""+ loggedInUserEmail+"\"");
                if (loggedInUserEmail != null){
                    Console.WriteLine("Useremail not empty");
                    PersonValidation = await PersonService!.ReadPersonEmail(loggedInUserEmail);
                    await updateListsFromDatabase();

                    personAssignmentId = (await PersonAssignmentService!.ReadPersonAssignment(PersonValidation.PersonID)).PersonAssignmentId;
                    Console.WriteLine("personAssignmentId " + personAssignmentId);
                    await updateShiftsTable();
                }
                

            }
            catch
            {
                Console.WriteLine("No user logged in");
            }
            

           

        }


        public async void TakeShiftClicked(Shift shift) {
            Console.WriteLine("TakeShiftClicked");
            ShiftAssignment newShiftAssigned = new ShiftAssignment();
            int personid = PersonValidation.PersonID;


            newShiftAssigned.personassignmentid = personAssignmentId;
            newShiftAssigned.ShiftId = shift.ShiftID;

            await ShiftAssignmentService!.CreateShiftAssignment(newShiftAssigned);

            await updateListsFromDatabase();
            await updateShiftsTable();
        }

        public async void removeShiftClicked(int shiftId) {
            int ShiftAssignmentidTmp = findShiftAssignmentID(shiftId);
            await ShiftAssignmentService!.DeleteShiftAssignment(ShiftAssignmentidTmp);
            await updateListsFromDatabase();
            await updateShiftsTable();
        }

        public Task updateShiftsTable() {
            Console.WriteLine("updateShiftsTable");
            personShiftsList.Clear();
            ListOfPersonAreaShifts.Clear();
            foreach (Shift shift in listOfAllShifts) {
                
                if (shift.areaId == PersonValidation.areaId) {
                    
                    foreach (ShiftAssignment shiftAssignment in listOfShiftAssignments) {

                        if (shiftAssignment.ShiftId == shift.ShiftID && personAssignmentId == shiftAssignment.personassignmentid) {
                            shift.backgroundColor = "red";
                            personShiftsList.Add(shift);
                        }
                    }
                    if (shift.IsLocked == true){
                        shift.backgroundColor = "grey";
                    }
                    ListOfPersonAreaShifts.Add(shift);
                }
            }

            ListOfPersonAreaShifts = ListOfPersonAreaShifts.OrderBy((x) => x.StartTime).ToList();
            calculatePersonPoints();
            Console.WriteLine("ListOfPersonAreaShifts count: " + ListOfPersonAreaShifts.Count());
            StateHasChanged();
            return Task.CompletedTask;
        }
        public async Task updateListsFromDatabase()
        {
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            listOfShiftAssignments = (await ShiftAssignmentService!.ReadAllShiftAssignments())!.ToList();

            foreach (Shift shift in listOfAllShifts)
            {
                shift.calculateShiftPoints();
            }

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

        public string convertDay(int i)
        {
            switch (i)
            {
                case 19:
                    return "Friday";
                case 20:
                    return "Saturday";
                case 21:
                    return "Sunday";
                default:
                    return "";
            }
        }


        public void calculatePersonPoints() {
            Console.WriteLine("calculatePersonPoints");
            personTotalPoints = 0.0f;

            foreach (Shift shift1 in personShiftsList)
            {
                personTotalPoints += shift1.shiftPoints;
            }

            Console.WriteLine("personTotalPoints: " + personTotalPoints);

        }

        public async void UserLogOut()
        {
            await localStore.ClearAsync();
            navigationManager.NavigateTo("/");
        }

    }



}

