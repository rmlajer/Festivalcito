using System;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Festivalcito.Client.Pages{

	partial class Coordinator_VolunteerPage{

        [Inject]
        public IPersonService? PersonService { get; set; }

        List<Person> listOfAllPeople = new List<Person>();

        public string emailInput = "bob@mail.com";

        private Person PersonValidation = new Person();

        private EditContext? EditContext;

        public Coordinator_VolunteerPage()
		{
		}

        protected override void OnInitialized()
        {
            base.OnInitialized();
            //PersonValidation.area = new Area();
            EditContext = new EditContext(PersonValidation);
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

