using System;
using Microsoft.AspNetCore.Components.Forms;
using Festivalcito.Shared.Classes;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Client.Services.LoginCredentialService;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Text;
using System.Security.Cryptography;


namespace Festivalcito.Client.Pages
{

    partial class Registration_LoginPage
    {

        [Inject]
        public IPersonService? PersonService { get; set; }

        [Inject]
        public ILoginCredentialService? LoginCredentialService { get; set; }

        //Bruges i HTML til at sætte Bootstrap klassen på InputField div
        public string divClassInputField = "mb-1";
        //Opretter Person til validering i EditForm
        private Person PersonValidation = new Person();
        //Opretter nyt LoginCredential til validering i EditForm
        private LoginCredential LoginValidation = new LoginCredential();
        //Opretter EditContextLogin til anvendelse af EditForm
        private EditContext? EditContextLogin;
        //Opretter EditContextRegistration til anvendelse af EditForm
        private EditContext? EditContextRegistration;

        public Registration_LoginPage()
        {
        }

        //Opdaterer LocalStore baseret på email fra User Login
        public async void UpdateLocalStorage()
        {
            await localStore.SetItemAsync("loggedInUserId", LoginValidation.UserEmail);
        }


        //Køres når siden indlæses, sætter variabler til anvendelse i EditForms, samt fødselsdato til præsentation
        protected override void OnInitialized()
        {
            EditContextLogin = new EditContext(LoginValidation);
            EditContextRegistration = new EditContext(PersonValidation);
            PersonValidation.DateOfBirth = DateTime.Today;
        }


        //Håndterer accepteret LoginValidation i registration EditForm.
        //Sender Create HTTP til DB via Controller, sætter localStore med bruger email og navigerer til volunteerPage
        //Hvis Create HTTP er succesfuldt sendes HTTP Create på LoginCredential til DB via Controller
        public async void HandleValidRegistrationSubmit()
        {
            Console.WriteLine("submitClicked");
            int statusCode = await PersonService!.CreatePerson(PersonValidation);
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
            }

        }

        //Håndterer accepteret LoginValidation og sender Create HTTP til DB via Controller
        //Sætter localStore med bruger email
        //Hvis isCoordinator == False navigerer til volunteerPage
        //Hvis isCoordinator == True navigerer til Coordinator_ShiftPage
        public async Task<bool> HandleValidLoginSubmit()
        {

            if (LoginValidation.UserEmail != "" && LoginValidation.HashedPassword != "")
            {
                LoginCredential loginCredential = (await LoginCredentialService!.ReadLoginCredential(LoginValidation.UserEmail!));
                LoginValidation.loginResponse = loginCredential.loginResponse;

                if (LoginValidation.UserEmail == loginCredential.UserEmail &&
                    Sha1(LoginValidation.HashedPassword!) == loginCredential.HashedPassword)
                {

                    //local storage save
                    LoginValidation.loginResponse = "Login successful";
                    await localStore.SetItemAsync("userLoggedInEmail", LoginValidation.UserEmail!);

                    //Henter Person fra DB
                    Person signedInPerson = (await PersonService!.ReadPersonEmail(loginCredential.UserEmail));

                    //Tjekker isCoordinator
                    if (signedInPerson.IsCoordinator == true)
                    {
                        navigationManager.NavigateTo("/Coordinator_ShiftPage");
                    }
                    else
                    {
                        navigationManager.NavigateTo("/volunteerPage");
                    }

                    return true;
                }
                else
                {
                    LoginValidation.loginResponse = "Login failed";
                    return false;
                }

            }
            else
            {
                LoginValidation.loginResponse = "Type in both username and password";
            }
            return false;


        }

        //Hasher password ved hjælp af Sha1 algoritme. 
        public static string Sha1(string input)
        {
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

