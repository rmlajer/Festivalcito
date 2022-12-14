using System;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Client.Services.ShiftServicesFolder;
using Festivalcito.Client.Services.ShiftAssignedServicesFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Festivalcito.Client.Pages{

	partial class VolunteerPage{

        [Inject]
        public IPersonService? PersonService { get; set; }
        [Inject]
        public IShiftService? ShiftService { get; set; }
        [Inject]
        public IShiftAssignedService? ShiftAssignedService { get; set; }


        List<Person> listOfAllPeople = new List<Person>();
        List<Shift> listOfAllShifts = new List<Shift>();
        List<ShiftAssigned> listOfAssignedShifts = new List<ShiftAssigned>();

        List<Shift> ListOfPersonAreaShifts = new List<Shift>();

        public string emailInput = "bob@mail.com";

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
            listOfAllPeople = (await PersonService!.ReadAllPersons())!.ToList();
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            listOfAssignedShifts = (await ShiftAssignedService!.ReadAllShiftAssigned())!.ToList();
        }





        public async void getUserInfo(string email){
            foreach (Person person in listOfAllPeople){
                Console.WriteLine(person.ToString());
                if (person.EmailAddress!.ToLower() == emailInput.ToLower()){
                    Person shiftPerson = new Person();
                    PersonValidation = person;

                    shiftPerson = (await PersonService!.ReadPersonJoinArea(person.PersonID));


                    Console.WriteLine("Add correct shifts to list");
                    Console.WriteLine("shiftPerson.areaName = " + shiftPerson.areaName);
                    foreach (Shift shift in listOfAllShifts)
                    {
                        Console.WriteLine("shift.areaName" + shift.ToString());
                        if (shift.areaName == shiftPerson.areaName)
                        {
                            ListOfPersonAreaShifts.Add(shift);
                        }
                    }
                }
            }
            Console.WriteLine(PersonValidation.ToString());


        }
    }

    







}

