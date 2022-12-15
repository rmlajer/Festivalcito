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
        List<Shift> listOfAllShifts = new List<Shift>();

        private string submitButtonText = "Create";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContextShift = new EditContext(ShiftValidation);
            ShiftValidation.areaId = 4;
            
        }

        protected override async Task OnInitializedAsync()
        {
            AllAreas = (await AreaService!.ReadAllAreas())!.ToList();
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
        }

        private async void HandleValidSubmit()
        {
            Console.WriteLine("HandleValidSubmit");
            Console.WriteLine("ShiftValidation.ShiftID: " + ShiftValidation.ShiftID);
            if (ShiftValidation.ShiftID == 0){
                await ShiftService!.CreateShift(ShiftValidation);
            }
            else
            {
                await ShiftService!.UpdateShift(ShiftValidation);
            }

            await updateLists();
            
        }

        private void HandleInValidSubmit()
        {
            Console.WriteLine(ShiftValidation.ToString());
        }



        private async Task updateLists()
        {
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            StateHasChanged();
        }

        private void HandleInvalidSubmit(){
            Console.WriteLine("HandleInvalidSubmit Called...");
        }

        public void selectShift(Shift shift)
        {

            ShiftValidation = shift;
            EditContextShift = new EditContext(shift);
            submitButtonText = "Update";
        }

        public async void deleteShift(int shiftId)
        {
            await ShiftService!.DeleteShift(shiftId);
            await updateLists();
        }


    }
}

