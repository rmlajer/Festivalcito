using Festivalcito.Server.Models.PersonRepositoryFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Festivalcito.Server.Controllers{
	
    [ApiController]
    [Route("api/person/")]
    public class PersonController : ControllerBase{

        private readonly IPersonRepository Repository = new PersonRepository();


        //contructor tjekker om repository er tom og hvis den er, laves et nyt
        public PersonController(IPersonRepository personRepository)
        {
            if (Repository == null && personRepository != null)
            {
                Repository = personRepository;
                Console.WriteLine("Repository initialized");
            }
        }

        // Kalder CreatePerson i PersonRepository
        // Opretter Person i DB
        [HttpPost]
        public void CreatePerson(Person person)
        {
            Console.WriteLine("Controller - " + person.FirstName);
            Repository.CreatePerson(person);
        }

        // Kalder ReadPerson i PersonRepository
        //Den kaldte metode henter specifik Person fra DB baseret på PersonID
        [HttpGet("{id:int}")]
        public Person ReadPerson(int id)
        {
            Console.WriteLine("Controller - ReadPerson");
            return Repository.ReadPerson(id);
        }

        [HttpGet("joinarea/{id:int}")]
        public Person ReadPersonJoinArea(int id)
        {
            Console.WriteLine("Controller - ReadPersonJoinArea");
            Console.WriteLine("id : " + id);
            return Repository.ReadPersonJoinArea(id);
        }


        //Kalder ReadAllPerson i PersonRepository
        // Henter alle Persons fra DB
        [HttpGet]
        public IEnumerable<Person> ReadAllPersons()
        {
            Console.WriteLine("Controller - ReadAllPersons");
            return Repository.ReadAllPersons();
        }

        // Kalder UpdatePerson i PersonRepository
        // Opdaterer Person i DB - Alle kolonner i en række bliver opdateret
        [HttpPut]
        public void UpdatePerson(Person person)
        {
            Repository.UpdatePerson(person);
        }


        // Kalder DeletePerson i PersonRepository
        // Sletter specifik Person på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeletePerson(int id)
        {
            Console.WriteLine("Server: Delete item called: id = " + id);

            bool deleted = Repository.DeletePerson(id);
            if (deleted){
                Console.WriteLine("Server: Item deleted succces");
                int code = (int)HttpStatusCode.OK;
                return new StatusCodeResult(code);
            }
            else{
                Console.WriteLine("Server: Item deleted fail - not found");
                int code = (int)HttpStatusCode.NotFound;
                return new StatusCodeResult(code);
            }
        }

    }


}

