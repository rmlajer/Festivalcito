using System;
using Microsoft.AspNetCore.Components.Forms;
using Festivalcito.Shared.Classes;
using Festivalcito.Client.Services.PersonServicesFolder;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;

namespace Festivalcito.Client.Pages{

	partial class Registration_LoginPage{
        
        List<Person> listOfAllPeople = new List<Person>();

        [Inject]
        public IPersonService? PersonService { get; set; }

        public string divClassInputField = "mb-2";

        private Person PersonValidation = new Person();
        private LoginCredentials LoginValidation = new LoginCredentials();

        private EditContext? EditContextLogin;
        private EditContext? EditContextRegistration;

        public Registration_LoginPage(){
		}

        public async void UpdateLocalStorage()
        {
            await localStore.SetItemAsync("loggedInUserId", LoginValidation.UserEmail);
        }


        private void HandleValidSubmit(){
            
        }

        private void HandleInvalidSubmit(){
            Console.WriteLine("HandleInvalidSubmit Called...");
        }
        protected override void OnInitialized(){
            base.OnInitialized();
            EditContextLogin = new EditContext(LoginValidation);
            EditContextRegistration = new EditContext(PersonValidation);
            PersonValidation.DateOfBirth = DateTime.Today;

            PersonValidation.FirstName = "Dan";
            PersonValidation.LastName = "Brown";
            PersonValidation.Address = "Something avenue";
            PersonValidation.PhoneNumber = "52345235";
            PersonValidation.City = "Chicago";
            PersonValidation.Nationality = "Danish";
            PersonValidation.Country = "Portugal";
            PersonValidation.DanishLevel = 1;
            PersonValidation.PostalCode = "3623";
            PersonValidation.FirstName = "Dan";
            PersonValidation.Gender = "Male";
            PersonValidation.EmailAddress = "Dan@mail.com";

        }

        protected override async Task OnInitializedAsync()
        {
            listOfAllPeople = (await PersonService!.ReadAllPersons())!.ToList();
        }

        public void submitClicked(){
            PersonService!.CreatePerson(PersonValidation);
        }

        public void LoginSubmitClicked(){
            if (LoginValidation.UserEmail != ""){
                Person loggedInPerson = new Person();
                foreach (Person personInList in listOfAllPeople)
                {
                    if (personInList.EmailAddress!.ToLower() == LoginValidation.UserEmail!.ToLower())
                    {
                        loggedInPerson = personInList;
                        //local storage save
                        localStore.SetItemAsync("userLoggedInEmail", loggedInPerson.EmailAddress);
                    }
                }
            }
        }

    }
}

