using System;
using Festivalcito.Client.Services.AreaServicesFolder;
using Festivalcito.Client.Services.ShiftServicesFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Festivalcito.Client.Pages
{
	partial class Coordinator_ShiftPage
	{
		public Coordinator_ShiftPage()
		{
		}

        [Inject]
        public IAreaService? AreaService { get; set; }
        [Inject]
        public IShiftService? ShiftService { get; set; }

        private EditContext? EditContextShift;
        private Shift ShiftValidation = new Shift();

        private List<Area> AllAreas = new List<Area>();

        private string submitButtonText = "Create";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContextShift = new EditContext(ShiftValidation);
            
        }

        protected override async Task OnInitializedAsync()
        {
            AllAreas = (await AreaService!.ReadAllAreas())!.ToList();
        }

        private void HandleValidSubmit()
        {
            Console.WriteLine("HandleValidSubmit");
            Console.WriteLine("ShiftValidation.ShiftID: " + ShiftValidation.ShiftID);
            if (ShiftValidation.ShiftID == 0)
            {
                ShiftService!.CreateShift(ShiftValidation);
            }
        }

        private void HandleInvalidSubmit(){
            Console.WriteLine("HandleInvalidSubmit Called...");
        }


    }
}

