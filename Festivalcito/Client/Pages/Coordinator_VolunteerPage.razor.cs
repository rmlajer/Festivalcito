using System;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Client.Services.AreaServicesFolder;
using Festivalcito.Client.Services.PersonAssignmentServicesFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Blazored.LocalStorage;
using System.Reflection.Metadata;

namespace Festivalcito.Client.Pages
{

    partial class Coordinator_VolunteerPage
    {

        [Inject]
        public IPersonService? PersonService { get; set; }

        [Inject]
        public IAreaService? AreaService { get; set; }

        [Inject]
        public IPersonAssignmentService? PersonAssignmentService { get; set; }

        //Liste af alle personer der ikke har PersonAssignment
        List<Person> GlobalList = new List<Person>();
        //Liste af alle personer med samme AreaId som koordinator på PersonAssignment 
        List<Person> ListOfAllPeopleOnArea = new List<Person>();
        //Personen som passer til loggedInUser i localStore
        private Person LoggedInPerson = new Person();

        public Coordinator_VolunteerPage()
        {
        }


        //Køres asynkront når siden indlæses
        protected override async Task OnInitializedAsync()
        {
            string email = await localStore.GetItemAsync<string>("userLoggedInEmail");
            LoggedInPerson = await PersonService!.ReadPersonEmail(email);
            //Opdaterer listen af personer på koordinatorens Area fra DB via Controller
            UpdateListOfAllPeopleOnArea((await PersonService!.ReadAllPersons())!.ToList());

        }

        //Sender HTTP Create til DB via Controller med ny personAssignment opdaterer så 
        public async void AddUserToCoordinatorList(Person person)
        {
            PersonAssignment personAssignment = new PersonAssignment();
            personAssignment.AreaId = LoggedInPerson.areaId;
            personAssignment.personid = person.PersonID;
            await PersonAssignmentService!.CreatePersonAssignment(personAssignment);
            UpdateListOfAllPeopleOnArea((await PersonService!.ReadAllPersons())!.ToList());
        }

        //Sender HTTP Delete til DB via Controller og opdaterer listen af personer
        public async void RemoveUserFromCoordinatorList(Person person)
        {
            //Koordinator kan ikke slettes
            if (person.IsCoordinator == false)
            {
                int tmpPersonAssignmentId = (await PersonAssignmentService!.ReadPersonAssignment(person.PersonID)).PersonAssignmentId;
                await PersonAssignmentService.DeletePersonAssignment(tmpPersonAssignmentId);
                UpdateListOfAllPeopleOnArea((await PersonService!.ReadAllPersons())!.ToList());
            }


        }

        //Opdaterer listen af personer på koordinatorens Area
        public async void UpdateListOfAllPeopleOnArea(List<Person> dbList)
        {
            //Henter alle personAssignments fra DB
            List<PersonAssignment> PersonAssignmentsDB = (await PersonAssignmentService!.ReadAllPersonAssignments())!.ToList();

            ListOfAllPeopleOnArea.Clear();
            GlobalList.Clear();

            //Tilknytter AreaId til Person object fra personAssignment
            foreach (PersonAssignment assignment in PersonAssignmentsDB)
            {
                foreach (Person person1 in dbList)
                {
                    if (person1.PersonID == assignment.personid)
                    {
                        person1.areaId = assignment.AreaId;
                    }
                }
            }

            //Tilføjer Person til Listen af personer på Koordinatorens Area hvis AreaId matcher
            //Hvis AreaId er 0 tilføjes Person til GlobalList
            foreach (Person person1 in dbList)
            {
                if (person1.areaId == LoggedInPerson.areaId && person1.PersonID != LoggedInPerson.PersonID)
                {

                    ListOfAllPeopleOnArea.Add(person1);
                }
                else if (person1.areaId == 0)
                {
                    GlobalList.Add(person1);
                }
            }

            //Beder Blazor opdatere HTML efter lister opdateret
            StateHasChanged();
        }


        public void SendEmailToUser(Person person)
        {
            PersonService!.SendEmailToPerson(person);
        }
        public async void UserLogOut()
        {
            await localStore.ClearAsync();
            navigationManager.NavigateTo("/");
        }


    }

}


