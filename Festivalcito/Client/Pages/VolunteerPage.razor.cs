using System;
using Festivalcito.Client.Services.PersonServicesFolder;
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


        List<Person> listOfAllPeople = new List<Person>();
        List<Shift> listOfAllShifts = new List<Shift>();
        List<ShiftAssignment> listOfAssignedShifts = new List<ShiftAssignment>();

        List<Shift> ListOfPersonAreaShifts = new List<Shift>();

        public string loggedInUserEmail = "";
        Person loggedInPerson = new Person();

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
            listOfAssignedShifts = (await ShiftAssignmentService!.ReadAllShiftAssigned())!.ToList();
            Console.WriteLine("listOfAllShifts count: " + listOfAllShifts.Count());
            Console.WriteLine("PersonValidation areaName: " + PersonValidation.areaName);


            foreach (Shift shift in listOfAllShifts)
            {
                
                if (shift.areaName == PersonValidation.areaName)
                {
                    ListOfPersonAreaShifts.Add(shift);

                }
            }

            Console.WriteLine("ListOfPersonAreaShifts count: " + ListOfPersonAreaShifts.Count());

            
        }


        public async void TakeShiftClicked()
        {
            Console.WriteLine("TakeShiftClicked");
            ShiftAssignment newShiftAssigned = new ShiftAssignment();
            newShiftAssigned.personassignmentid = 1;
            newShiftAssigned.ShiftAssignmentid = 1;
            await ShiftAssignmentService!.CreateShiftAssigned(newShiftAssigned);
        }


    }

    







}

