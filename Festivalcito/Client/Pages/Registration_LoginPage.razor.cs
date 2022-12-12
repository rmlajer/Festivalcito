using System;
using Microsoft.AspNetCore.Components.Forms;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Pages{

	partial class Registration_LoginPage{

        private Person PersonValidation = new Person();
        private LoginCredentials LoginValidation = new LoginCredentials();

        private EditContext? EditContextLogin;
        private EditContext? EditContextRegistration;

        public Registration_LoginPage()
		{
		}


        private void HandleValidSubmit()
        {

        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContextLogin = new EditContext(LoginValidation);
        }

    }
}

