using System;
using Microsoft.AspNetCore.Components.Forms;
using Festivalcito.Shared.Classes;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Client.Services.LoginCredentialService;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Text;
using System.Security.Cryptography;

namespace Festivalcito.Client.Pages{

	partial class Registration_LoginPage{
        
        List<Person> listOfAllPeople = new List<Person>();

        [Inject]
        public IPersonService? PersonService { get; set; }

        [Inject]
        public ILoginCredentialService? LoginCredentialService { get; set; }

        public string divClassInputField = "mb-2";

        private Person PersonValidation = new Person();
        private LoginCredential LoginValidation = new LoginCredential();

        private EditContext? EditContextLogin;
        private EditContext? EditContextRegistration;

        public Registration_LoginPage(){
		}

        public async void UpdateLocalStorage()
        {
            await localStore.SetItemAsync("loggedInUserId", LoginValidation.UserEmail);
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

        public async void LoginSubmitClicked(){
            Console.WriteLine("LoginSubmitClicked");
            if (LoginValidation.UserEmail != "" && LoginValidation.HashedPassword != ""){
                LoginCredential loginCredential = (await LoginCredentialService!.ReadLoginCredential(LoginValidation.UserEmail!));

                if (LoginValidation.UserEmail == loginCredential.UserEmail &&
                    Sha1(LoginValidation.HashedPassword!) == loginCredential.HashedPassword){
                    Console.WriteLine("Login succes");
                    //local storage save
                    await localStore.SetItemAsync("userLoggedInEmail", LoginValidation.UserEmail!);
                }

            }
        }

        public static string Sha1(string input){
            using var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

    }
}

