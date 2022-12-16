using System;
using System.Reflection;
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

        //Alle tilgængelige area's i systemet
        private List<Area> AllAreas = new List<Area>();
        //Alle tilgængelige vagter i systemet
        List<Shift> listOfAllShifts = new List<Shift>();
        //Vagter som skal indeholde vagter på det valgte area
        List<Shift> PresentedShiftsList = new List<Shift>();

        List<Shift> ShiftsSortedByPoints = new List<Shift>();
        List<Shift> ShiftsSortedByVolunteers = new List<Shift>();

        private string submitButtonText = "Create";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContextShift = new EditContext(ShiftValidation);
            ShiftValidation.areaId = 2;
            
        }

        protected override async Task OnInitializedAsync()
        {
            AllAreas = (await AreaService!.ReadAllAreas())!.ToList();
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            await updatePresentedShiftsList();
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

            await updateListsFromDatabase();
            
        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine(ShiftValidation.ToString());
        }

        public async Task updatePresentedShiftsList(){
            Console.WriteLine("updatePresentedShiftsList");
            PresentedShiftsList.Clear();
            foreach (Shift shift in listOfAllShifts){
                if (ShiftValidation.areaId == shift.areaId)
                {
                    PresentedShiftsList.Add(shift);
                }
            }
          
    
           

        }

        private async Task updateListsFromDatabase(){
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            await updatePresentedShiftsList();
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
            await updateListsFromDatabase();
        }


    }
}

