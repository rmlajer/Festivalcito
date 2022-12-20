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

        public string divClassInputField = "mb-1";

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
            Console.WriteLine("submitClicked");
            int statusCode =  await PersonService!.CreatePerson(PersonValidation);
            Console.WriteLine("statusCode: " + statusCode);

            if (statusCode == 200)
            {
                LoginValidation.HashedPassword = Sha1(LoginValidation.HashedPassword!);
                LoginValidation.UserEmail = PersonValidation.EmailAddress!;
                LoginValidation.loginResponse = "Create successful";
                await LoginCredentialService!.CreateLoginCredentials(LoginValidation);
                await localStore.SetItemAsync("userLoggedInEmail", LoginValidation.UserEmail!);
                navigationManager.NavigateTo("/volunteerPage");
            }
            else
            {
                LoginValidation.loginResponse = "Create failed";
                //return false;
            }

        }

        public async Task<bool> LoginSubmitClicked(){
            Console.WriteLine("LoginSubmitClicked");
            Console.WriteLine("LoginValidation.UserEmail: " + "\"" + LoginValidation.UserEmail + "\"");
            if (LoginValidation.UserEmail != "" && LoginValidation.HashedPassword != ""){
                LoginCredential loginCredential = (await LoginCredentialService!.ReadLoginCredential(LoginValidation.UserEmail!));
                LoginValidation.loginResponse = loginCredential.loginResponse;
                Console.WriteLine("Response from SQL: " + loginCredential.loginResponse);
                if (LoginValidation.UserEmail == loginCredential.UserEmail &&
                    Sha1(LoginValidation.HashedPassword!) == loginCredential.HashedPassword){
                    Console.WriteLine("Login succes");
                    //local storage save
                    LoginValidation.loginResponse = "Login successful";
                    await localStore.SetItemAsync("userLoggedInEmail", LoginValidation.UserEmail!);

                    Person signedInPerson = (await PersonService!.ReadPersonEmail(loginCredential.UserEmail));
                    Console.WriteLine("Test:" + signedInPerson.FirstName);
                    if (signedInPerson.IsCoordinator == true){
                        navigationManager.NavigateTo("/Coordinator_ShiftPage");
                    }else{
                        navigationManager.NavigateTo("/volunteerPage");
                    }

                    return true;
                }else
                {
                    LoginValidation.loginResponse = "Login failed";
                    return false;
                }
                
            }else
            {
                LoginValidation.loginResponse = "Type in both username and password";
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

