using System;
using System.Reflection;
using Festivalcito.Client.Services.AreaServicesFolder;
using Festivalcito.Client.Services.ShiftServicesFolder;
using Festivalcito.Client.Services.ShiftAssignmentServicesFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection.Metadata;

namespace Festivalcito.Client.Pages{
	partial class Coordinator_ShiftPage{
		public Coordinator_ShiftPage()
		{
		}

        [Inject]
        public IAreaService? AreaService { get; set; }
        [Inject]
        public IShiftService? ShiftService { get; set; }
        [Inject]
        public IShiftAssignmentService? ShiftAssignmentService { get; set; }

        private EditContext? EditContextShift;
        private Shift ShiftValidation = new Shift();

        //Alle tilgængelige area's i systemet
        private List<Area> AllAreas = new List<Area>();
        //Alle tilgængelige vagter i systemet
        List<Shift> listOfAllShifts = new List<Shift>();
        List<ShiftAssignment> listOfAllShiftAssignment = new List<ShiftAssignment>();
        //Vagter som skal indeholde vagter på det valgte area
        List<Shift> PresentedShiftsList = new List<Shift>();

        List<Shift> ShiftsSortedByPoints = new List<Shift>();
        List<Shift> ShiftsSortedByVolunteers = new List<Shift>();

        private string submitButtonText = "Create";

        protected override void OnInitialized()
        {
            EditContextShift = new EditContext(ShiftValidation);
            ShiftValidation.StartTime = DateTime.Today;
            ShiftValidation.EndTime = DateTime.Today;
        }

        protected override async Task OnInitializedAsync()
        {
            AllAreas = (await AreaService!.ReadAllAreas())!.ToList();
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            updatePresentedShiftsList(2);
            listOfAllShiftAssignment = (await ShiftAssignmentService!.ReadAllShiftAssignments())!.ToList();
            foreach (Shift shift in listOfAllShifts)
            {
                shift.calculateMissingPeople(listOfAllShiftAssignment);
            }
        }

        private async void HandleValidSubmit()
        {
            Console.WriteLine("HandleValidSubmit");
            Console.WriteLine("ShiftValidation.ShiftID: " + ShiftValidation.ShiftID);
            if (ShiftValidation.ShiftID == 0){
                await ShiftService!.CreateShift(ShiftValidation);
                await updateListsFromDatabase();
            }
            else
            {
                await ShiftService!.UpdateShift(ShiftValidation);
                await updateListsFromDatabase();
            }
            
            
        }

        public void updatePresentedShiftsList(int areaId){
            Console.WriteLine("updatePresentedShiftsList");
            ShiftValidation = new Shift();
            ShiftValidation.areaId = areaId;
            PresentedShiftsList.Clear();
            foreach (Shift shift in listOfAllShifts){
                Console.WriteLine(shift.ShiftName);
                if (ShiftValidation.areaId == shift.areaId)
                {
                    PresentedShiftsList.Add(shift);
                }
            }
            PresentedShiftsList = sortList("native");
            StateHasChanged();
        }

        public List<Shift> sortList(string sortType){
            foreach (Shift shift in PresentedShiftsList){ shift.calculateShiftPoints(); };

            if (sortType == "shiftPoints")
            {
                PresentedShiftsList = PresentedShiftsList.OrderByDescending(o => o.shiftPoints).ToList();
            }else if (sortType == "RequiredVolunteers")
            {
                Console.WriteLine("RequiredVolunteers");
                PresentedShiftsList = PresentedShiftsList.OrderBy(o => (o.calculateMissingPeople(listOfAllShiftAssignment))).ToList();
            }
            return PresentedShiftsList;
        }


        private async Task updateListsFromDatabase(){
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            updatePresentedShiftsList(ShiftValidation.areaId);
        }

        public void selectShift(Shift shift){
            ShiftValidation = shift;
            EditContextShift = new EditContext(shift);
            submitButtonText = "Update in datebase";
        }

        public async void deleteShift(int shiftId){
            await ShiftService!.DeleteShift(shiftId);
            await updateListsFromDatabase();
        }

        public async void UserLogOut(){
            await localStore.ClearAsync();
            navigationManager.NavigateTo("/");
        }




    }
}

