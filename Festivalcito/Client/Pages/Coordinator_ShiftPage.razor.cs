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

        //Initialiserer EditContext til anvendelse af EditForm
        private EditContext? EditContextShift;
        //Holder midlertidigt data til validering inden data sendes til Controller via Service
        private Shift ShiftValidation = new Shift();
        //Indeholder valgt Area fra bruger, sættes som standard til første Area.AreaId i AllAreas efter til sidst i OnInitializedAsync();
        public int userChosenArea = 0;
        //Alle tilgængelige area's i systemet
        private List<Area> AllAreas = new List<Area>();
        //Alle tilgængelige vagter i systemet
        List<Shift> ListOfAllShifts = new List<Shift>();
        //Alle tilgængelige ShiftAssignments i systemet
        List<ShiftAssignment> ListOfAllShiftAssignment = new List<ShiftAssignment>();
        //Vagter som skal indeholde vagter på det valgte area
        List<Shift> PresentedShiftsList = new List<Shift>();
        //String til at indeholde den præsenterede text på Submit button
        private string submitButtonText = "Create";

        //Køres når siden indlæses
        protected override void OnInitialized()
        {
            //Opretter EditContext til anvendelse i EditForm,
            EditContextShift = new EditContext(ShiftValidation);

            //sætter præsenteret Start og Slut tid på ShiftValidation
            ShiftValidation.StartTime = DateTime.Parse("2023-05-01T08:00");
            ShiftValidation.EndTime = DateTime.Parse("2023-05-01T16:00");
        }

        //Køres asynkront når siden indlæses
        protected override async Task OnInitializedAsync()
        {
            //Henter alle Areas fra DB
            AllAreas = (await AreaService!.ReadAllAreas())!.ToList();
            //Henter alle Shifts fra DB
            ListOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            //Henter alle ShiftAssignments fra DB
            ListOfAllShiftAssignment = (await ShiftAssignmentService!.ReadAllShiftAssignments())!.ToList();
            
            userChosenArea = AllAreas.First().AreaID;
            //Opdaterer præsenterede Shifts
            UpdatePresentedShiftsList(userChosenArea);

            //Udregner manglende Personer på hvert Shift
            foreach (Shift shift in ListOfAllShifts)
            {
                shift.CalculateMissingPeople(ListOfAllShiftAssignment);
            }

            //Sætter userChosenAra til første area i AllAreas.
            
            
        }


        //Håndterer accepteret Submit fra EditForm.
        private async Task HandleValidSubmit()
        {
            
            //Tjekker om ShiftValidation indeholder et nyt eller eksisterende Shift

            //Hvis ShiftValidation er et nyt Shift sendes Create HTTP til DB via Controller
            if (ShiftValidation.ShiftID == 0){
                ShiftValidation.areaId = userChosenArea;
                await ShiftService!.CreateShift(ShiftValidation);
                await UpdateListsFromDatabase();
                ShiftValidation = new Shift();
                ShiftValidation.StartTime = DateTime.Parse("2023-05-01T08:00");
                ShiftValidation.EndTime = DateTime.Parse("2023-05-01T16:00");
            }
            //Hvis ShiftValidation er et eksisterende Shift sendes Update HTTP til DB via Controller
            else
            {
                ShiftValidation.areaId = userChosenArea;
                await ShiftService!.UpdateShift(ShiftValidation);
                await UpdateListsFromDatabase();
                ShiftValidation = new Shift();
                ShiftValidation.StartTime = DateTime.Parse("2023-05-01T08:00");
                ShiftValidation.EndTime = DateTime.Parse("2023-05-01T16:00");
                submitButtonText = "Create";
            }
        }


        //Opdaterer præsenterede Shifts baseret på userChosenArea og sorterer disse. 
        
        public void UpdatePresentedShiftsList(int areaId){
            Console.WriteLine("updatePresentedShiftsList");
            
            userChosenArea = areaId;
            PresentedShiftsList.Clear();
            foreach (Shift shift in ListOfAllShifts){
                Console.WriteLine(shift.ShiftName);
                if (userChosenArea == shift.areaId)
                {
                    PresentedShiftsList.Add(shift);
                }
            }
            SortPresentedShiftsList("native");            
        }

        //Sorterer præsenterede Shifts baseret på valgt parameter
        public void SortPresentedShiftsList(string sortType){
            foreach (Shift shift in PresentedShiftsList){ shift.CalculateShiftPoints(); };

            if (sortType == "shiftPoints")
            {
                PresentedShiftsList = PresentedShiftsList.OrderByDescending(o => o.shiftPoints).ToList();
            }else if (sortType == "RequiredVolunteers"){
                Console.WriteLine("RequiredVolunteers");
                PresentedShiftsList = PresentedShiftsList.OrderBy(o => (o.CalculateMissingPeople(ListOfAllShiftAssignment))).ToList();
            }
        }

        //Henter alle Shifts fra DB, opdaterer derefter præsenterede Shifts baseret på disse og brugeren valgte Area
        private async Task UpdateListsFromDatabase(){
            ListOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            UpdatePresentedShiftsList(userChosenArea);
        }

        //Sætter ShiftValidation til det valgte Shift
        public void SelectShift(Shift shift){
            ShiftValidation = shift;
            EditContextShift = new EditContext(ShiftValidation);
            submitButtonText = "Update in datebase";
        }

        //Sender Delete HTTP til DB via Controller baseret på valgt Shift
        public async void DeleteShift(int shiftId){
            await ShiftService!.DeleteShift(shiftId);
            await UpdateListsFromDatabase();
        }

        //Fjerner gemt data i localStore, samt sender bruger til Index.
        public async void UserLogOut(){
            await localStore.ClearAsync();
            navigationManager.NavigateTo("/");
        }




    }
}

