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

        [Inject]
        public IPersonService? PersonService { get; set; }

        [Inject]
        public ILoginCredentialService? LoginCredentialService { get; set; }

        public string divClassInputField = "mb-2";
        public string responseMessage = "";

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

            /*
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
            */
        }

        public async void submitClicked(){
            await PersonService!.CreatePerson(PersonValidation);
            LoginValidation.HashedPassword = Sha1(LoginValidation.HashedPassword!);
            LoginValidation.UserEmail = PersonValidation.EmailAddress;
            int statusCode = await LoginCredentialService!.CreateLoginCredentials(LoginValidation);
            Console.WriteLine("statusCode: " + statusCode);
            if (statusCode != 200)
            {
                responseMessage = "Create failed";
            }
            else
            {
                responseMessage = "Create succesfull";
            }

        }

        public async Task<bool> LoginSubmitClicked(){
            Console.WriteLine("LoginSubmitClicked");
            Console.WriteLine("LoginValidation.UserEmail: " + "\"" + LoginValidation.UserEmail + "\"");
            if (LoginValidation.UserEmail != "" && LoginValidation.HashedPassword != ""){
                Console.WriteLine("Why you here");
                Console.WriteLine("LoginValidation.HashedPassword: " + LoginValidation.HashedPassword!);
                LoginCredential loginCredential = (await LoginCredentialService!.ReadLoginCredential(LoginValidation.UserEmail!));
                if (LoginValidation.UserEmail == loginCredential.UserEmail &&
                    Sha1(LoginValidation.HashedPassword!) == loginCredential.HashedPassword){
                    Console.WriteLine("Login succes");
                    //local storage save
                    responseMessage = "Login successful";
                    await localStore.SetItemAsync("userLoggedInEmail", LoginValidation.UserEmail!);
                    return true;
                }
                else 
                {
                    responseMessage = "Login failed";
                    return false;
                }
                
            }else
            {
                responseMessage = "Login failed";
            }
            return false;
            

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

