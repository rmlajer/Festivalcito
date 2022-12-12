using System;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Festivalcito.Client.Pages{



	partial class VolunteerPage{

        [Inject]
        public IPersonService? PersonService { get; set; }

        public string emailInput = "bob@mail.com";

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

        public async void getUserInfo(string email)
        {
            PersonValidation = (await PersonService!.ReadPersonEmail(email));
            Console.WriteLine(PersonValidation.ToString());
        }
    }

    







}

