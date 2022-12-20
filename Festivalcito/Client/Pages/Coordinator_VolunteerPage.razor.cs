using System;
using Festivalcito.Client.Services.PersonServicesFolder;
using Festivalcito.Client.Services.AreaServicesFolder;
using Festivalcito.Client.Services.PersonAssignmentServicesFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Blazored.LocalStorage;
using System.Reflection.Metadata;

namespace Festivalcito.Client.Pages{

    partial class Coordinator_VolunteerPage{

        [Inject]
        public IPersonService? PersonService { get; set; }

        [Inject]
        public IAreaService? AreaService { get; set; }

        [Inject]
        public IPersonAssignmentService? PersonAssignmentService { get; set; }


        List<Person> GlobalList = new List<Person>();
        List<Person> listOfAllPeopleOnArea = new List<Person>();

        private Person LoggedInPerson = new Person();

        public Coordinator_VolunteerPage()
        {
        }


        protected override async Task OnInitializedAsync()
        {
            string email = await localStore.GetItemAsync<string>("userLoggedInEmail");
            LoggedInPerson = await PersonService!.ReadPersonEmail(email);

            updateListOfAllPeopleOnArea((await PersonService!.ReadAllPersons())!.ToList());

        }

        public async void addUserToCoordinatorList(Person person)
        {
            PersonAssignment personAssignment = new PersonAssignment();
            personAssignment.AreaId = LoggedInPerson.areaId;
            personAssignment.personid = person.PersonID;
            await PersonAssignmentService!.CreatePersonAssignment(personAssignment);
            await PersonService!.UpdatePerson(person);
            updateListOfAllPeopleOnArea((await PersonService!.ReadAllPersons())!.ToList());
        }

        public async void removeUserFromCoordinatorList(Person person)
        {
            if (person.IsCoordinator == false)
            {
                int tmpPersonAssignmentId = (await PersonAssignmentService!.ReadPersonAssignment(person.PersonID)).PersonAssignmentId;
                await PersonAssignmentService.DeletePersonAssignment(tmpPersonAssignmentId);
                updateListOfAllPeopleOnArea((await PersonService!.ReadAllPersons())!.ToList());
            }

            
        }

        public async void updateListOfAllPeopleOnArea(List<Person> dbList)
        {
            List<Person> dbListWithArea = new List<Person>();
            List<PersonAssignment> personAssignmentsDB = (await PersonAssignmentService!.ReadAllPersonAssignments())!.ToList();

            listOfAllPeopleOnArea.Clear();
            GlobalList.Clear();

            Console.WriteLine("updateListOfAllPeopleOnArea");
            Console.WriteLine("dbList.count: " + dbList.Count());
            Console.WriteLine("LoggedInareaIdid: " + LoggedInPerson.areaId);


            foreach (PersonAssignment assignment in personAssignmentsDB){
                foreach (Person person1 in dbList){
                    if (person1.PersonID == assignment.personid){
                        person1.areaId = assignment.AreaId;
                    }
                }
            }

            foreach (Person person1 in dbList){
                if (person1.areaId == LoggedInPerson.areaId && person1.PersonID != LoggedInPerson.PersonID){
                    
                    listOfAllPeopleOnArea.Add(person1);
                }
                else if (person1.areaId == 0){
                    GlobalList.Add(person1);
                }
            }


                Console.WriteLine("listOfAllPeopleOnArea count:" + listOfAllPeopleOnArea.Count());
            Console.WriteLine("GlobalList count:" + GlobalList.Count());
            foreach (Person person in listOfAllPeopleOnArea)
            {
                Console.WriteLine(person.ToString());
            }


            StateHasChanged();
        }

        public void sendEmailToUser(Person person){
            PersonService!.SendEmailToPerson(person);
        }
        public async void UserLogOut()
        {
            await localStore.ClearAsync();
            navigationManager.NavigateTo("/");
        }


    }

}


