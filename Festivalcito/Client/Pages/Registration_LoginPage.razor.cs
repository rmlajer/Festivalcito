using System;
using Microsoft.AspNetCore.Components.Forms;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Pages{

	partial class Registration_LoginPage{

        private Person PersonValidation = new Person();
        private LoginCredentials LoginValidation = new LoginCredentials();

        private EditContext? EditContext;

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




    }
}

