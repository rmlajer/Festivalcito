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

        public Coordinator_VolunteerPage(){
		}


        protected override async Task OnInitializedAsync(){
            string email = await localStore.GetItemAsync<string>("userLoggedInEmail");
            LoggedInPerson = await PersonService!.ReadPersonEmail(email);

            updateListOfAllPeopleOnArea((await PersonService!.ReadAllPersons())!.ToList());
            
        }

        private void HandleValidSubmit(){

        }

        private void HandleInvalidSubmit(){
            Console.WriteLine("HandleInvalidSubmit Called...");
        }

        public async void addUserToCoordinatorList(Person person){
            PersonAssignment personAssignment = new PersonAssignment();
            personAssignment.AreaId = LoggedInPerson.areaId;
            personAssignment.personid = person.PersonID;
            await PersonAssignmentService!.CreatePersonAssignment(personAssignment);
            person.Assigned = true;
            await PersonService!.UpdatePerson(person);
            StateHasChanged();
        }

        public async void removeUserFromCoordinatorList()
        {

        }

        public async void updateListOfAllPeopleOnArea(List<Person> dbList){
            List<Person> dbListWithArea = new List<Person>();
            List<PersonAssignment> personAssignmentsDB = (await PersonAssignmentService!.ReadAllPersonAssignments())!.ToList();

            Console.WriteLine("updateListOfAllPeopleOnArea");
            Console.WriteLine("dbList.count: " + dbList.Count());
            Console.WriteLine("LoggedInareaIdid: " + LoggedInPerson.areaId);

            

            foreach (Person person1 in dbList){

                if (person1.Assigned == true){


                    foreach (PersonAssignment assignment in personAssignmentsDB)
                    {
                        if (person1.PersonID == assignment.personid)
                        {
                            person1.areaId = assignment.AreaId;
                            if (person1.areaId == LoggedInPerson.areaId)
                            {
                                listOfAllPeopleOnArea.Add(person1);
                            }
                        }
                    }




                }
                else
                {
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

    }
}

