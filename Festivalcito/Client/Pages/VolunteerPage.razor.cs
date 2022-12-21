using System;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Client.Services.PersonAssignmentServicesFolder;
using Festivalcito.Client.Services.ShiftServicesFolder;
using Festivalcito.Client.Services.ShiftAssignmentServicesFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Blazored.LocalStorage;

namespace Festivalcito.Client.Pages
{

    partial class VolunteerPage
    {

        [Inject]
        public IPersonService? PersonService { get; set; }
        [Inject]
        public IShiftService? ShiftService { get; set; }
        [Inject]
        public IShiftAssignmentService? ShiftAssignmentService { get; set; }
        [Inject]
        public IPersonAssignmentService? PersonAssignmentService { get; set; }


        //Liste af alle shifts fra DB
        List<Shift> listOfAllShifts = new List<Shift>();
        //Liste af alle ShiftAssigments fra DB
        List<ShiftAssignment> listOfShiftAssignments = new List<ShiftAssignment>();
        //Liste af alle shifts der passer til aktiv Person fra DB
        List<Shift> ListOfShiftsOnPersonArea = new List<Shift>();
        //Liste af alle Shifts den aktive personer har ShiftAssignments på.
        List<Shift> ListOfTakenShifts = new List<Shift>();

        //Bruger email fra LocalStore
        public string? loggedInUserEmail { get; set; }
        int personAssignmentId = 0;
        float personTotalPoints = 0;

        //Opretter Person til validering i EditForm
        private Person PersonValidation = new Person();
        //Opretter EditContext til anvendelse af EditForm
        private EditContext? EditContext;



        public VolunteerPage()
        {
        }




        private void HandleValidSubmit()
        {
            PersonService!.UpdatePerson(PersonValidation);
        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(PersonValidation);
        }


        //Køres asynkront når siden indlæses
        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Henter aktiv bruger fra localStore
                loggedInUserEmail = await localStore.GetItemAsync<string>("userLoggedInEmail");

                //Hvis bruger eksisterer i localStore sendes Get HTTP til DB via Controller
                if (loggedInUserEmail != null)
                {
                    PersonValidation = await PersonService!.ReadPersonEmail(loggedInUserEmail);
                    await UpdateListsFromDatabase();
                    personAssignmentId = (await PersonAssignmentService!.ReadPersonAssignment(PersonValidation.PersonID)).PersonAssignmentId;
                    await UpdateShiftsTable();
                    EditContext = new EditContext(PersonValidation);
                }


            }
            catch
            {
                //Test i tilfælde af direkte tilgået side uden localstore data
                Console.WriteLine("No user logged in");
            }




        }


        //Opretter ShiftAssignment på valgt Shift og aktiv bruger.
        //Opdaterer lister fra DB via controller samt opdaterer Tabel i HTML. 
        public async void TakeShiftClicked(Shift shift)
        {
            Console.WriteLine("TakeShiftClicked");
            ShiftAssignment shiftAssignment = new ShiftAssignment();
            int personid = PersonValidation.PersonID;


            shiftAssignment.personassignmentid = personAssignmentId;
            shiftAssignment.ShiftId = shift.ShiftID;

            await ShiftAssignmentService!.CreateShiftAssignment(shiftAssignment);

            await UpdateListsFromDatabase();
            await UpdateShiftsTable();
        }


        //Fjerner shiftAssignment fra DB via Controller
        //Opdaterer lister fra DB via controller samt opdaterer Tabel i HTML. 
        public async void RemoveShiftClicked(int shiftId)
        {
            int ShiftAssignmentidTmp = FindShiftAssignmentID(shiftId);
            await ShiftAssignmentService!.DeleteShiftAssignment(ShiftAssignmentidTmp);
            await UpdateListsFromDatabase();
            await UpdateShiftsTable();
        }

        //Opdaterer tabel i HTML med FRESH data.
        public Task UpdateShiftsTable()
        {
            Console.WriteLine("updateShiftsTable");
            ListOfTakenShifts.Clear();
            ListOfShiftsOnPersonArea.Clear();

            //Løber alle shifts igennem i mod alle ShiftAssignments for at finde ud af om vagten skal markeres rød, grå eller grøn. 


            foreach (Shift shift in listOfAllShifts)
            {
                //Hvis AreaId på Shift og PersonValidation passer tilføjes Shift til ListOfShiftsOnPersonArea
                if (shift.areaId == PersonValidation.areaId)
                {

                    foreach (ShiftAssignment shiftAssignment in listOfShiftAssignments)
                    {


                        if (shiftAssignment.ShiftId == shift.ShiftID && personAssignmentId == shiftAssignment.personassignmentid)
                        {
                            shift.backgroundColor = "red";
                            //Hvis Shift, PersonAssignment og ShiftAssignment alle har samme Id tilføjes Shift til ListOfTakenShifts
                            ListOfTakenShifts.Add(shift);
                        }
                    }
                    if (shift.IsLocked == true)
                    {
                        shift.backgroundColor = "grey";
                    }
                    ListOfShiftsOnPersonArea.Add(shift);
                }
            }

            //Sorterer liste af Shifts baseret på startTime
            ListOfShiftsOnPersonArea = ListOfShiftsOnPersonArea.OrderBy((x) => x.StartTime).ToList();
            //Udregner aktiv persons shiftPoints
            CalculatePersonPoints();
            //Beder Blazor opdaterer HTML. 
            StateHasChanged();
            return Task.CompletedTask;
        }

        //Opdaterer list fra DB via Controllere. 
        public async Task UpdateListsFromDatabase()
        {
            listOfAllShifts = (await ShiftService!.ReadAllShifts())!.ToList();
            listOfShiftAssignments = (await ShiftAssignmentService!.ReadAllShiftAssignments())!.ToList();

            //Udregner points på Shifts i listOfAllShifts
            foreach (Shift shift in listOfAllShifts)
            {

                shift.CalculateShiftPoints();
            }

        }


        //Finder AssignmentId baseret på shiftId og den aktive persons personAssignmentId
        public int FindShiftAssignmentID(int shiftId)
        {
            foreach (ShiftAssignment shiftAssignment in listOfShiftAssignments)
            {
                if (shiftAssignment.ShiftId == shiftId && shiftAssignment.personassignmentid == personAssignmentId)
                {
                    return shiftAssignment.ShiftAssignmentid;
                }
            }
            return -1;
        }

        //Skriver dag baseret på dato i HTML
        public string ConvertDay(int i)
        {
            switch (i)
            {
                case 19:
                    return "Friday";
                case 20:
                    return "Saturday";
                case 21:
                    return "Sunday";
                default:
                    return "";
            }
        }

        //Udregner samlet antal point på aktiv bruger baseret på Shifts i ListOfAllTakenShifts
        public void CalculatePersonPoints()
        {

            personTotalPoints = 0.0f;

            foreach (Shift shift1 in ListOfTakenShifts)
            {
                personTotalPoints += shift1.shiftPoints;
            }


        }


        public async void UserLogOut()
        {
            await localStore.ClearAsync();
            navigationManager.NavigateTo("/");
        }

    }



}

