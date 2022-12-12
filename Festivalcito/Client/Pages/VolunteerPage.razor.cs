using System;
using Festivalcito.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Festivalcito.Client.Pages{



	partial class VolunteerPage{

        private Person PersonValidation = new Person();
        private EditContext? EditContext;

        public VolunteerPage(){


		}




        private void HandleValidSubmit()
        {

        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }

        protected override void OnInitialized(){
            base.OnInitialized();
            EditContext = new EditContext(PersonValidation);
        }
    }

    







}

