using System;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Festivalcito.Client.Pages{



	partial class VolunteerPage{

        [Inject]
        public IPersonService? PersonService { get; set; }

        List<Person> listOfAllPeople = new List<Person>();

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
        protected override async Task OnInitializedAsync()
        {
            listOfAllPeople = (await PersonService!.ReadAllPersons())!.ToList();
        }

        public void getUserInfo(string email)
        {
            foreach (Person person in listOfAllPeople)
            {
                Console.WriteLine(person.ToString());
                if (person.EmailAddress!.ToLower() == emailInput.ToLower())
                {
                    PersonValidation = person;
                }
            }
            Console.WriteLine(PersonValidation.ToString());


        }
    }

    







}

