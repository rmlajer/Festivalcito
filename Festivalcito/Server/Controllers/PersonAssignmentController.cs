using Festivalcito.Server.Models.PersonAssignmentRepositoryFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Festivalcito.Server.Controllers
{
    /// PersonAssignmentController håndterer kald af metoder som kommer fra services og kalder de respektive metoder i PersonAssignmentRepository
    /// Controlleren fungerer altså som bindeled og tillader os at skifte dele af programmet uden at ødelægge eksisterende struktur.

    [ApiController]
    [Route("api/assigned")]


    public class PersonAssignmentController : ControllerBase
    {
        private readonly IPersonAssignmentRepository Repository = new PersonAssignmentRepository();


        //Contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public PersonAssignmentController(IPersonAssignmentRepository personAssignmentRepository)
        {
            if (Repository == null && personAssignmentRepository != null)
            {
                Repository = personAssignmentRepository;
                Console.WriteLine("Repository initialized");
            }
        }

        // Kalder PersonAssignmentAssigned i PersonAssignmentRepository
        // Opretter PersonAssignment i DB
        [HttpPost]
        public void CreatePersonAssignment(PersonAssignment personAssignment)
        {

            Repository.CreatePersonAssignment(personAssignment);
        }

        // Kalder ReadAssigned i PersonAssignmentRepository
        //Den kaldte metode henter specifik PersonAssignment fra DB baseret på PersonAssignmentID
        [HttpGet("{id:int}")]
        public PersonAssignment ReadPersonAssignment(int id)
        {
            return Repository.ReadPersonAssignment(id);
        }


        //Kalder ReadAllPersonAssignment i PersonAssignmentRepository
        // Henter alle PersonAssignment fra DB
        [HttpGet]
        public IEnumerable<PersonAssignment> ReadAllPersonAssignments()
        {
            return Repository.ReadAllPersonAssignments();
        }


        // Kalder DeletePersonAssignment i PersonAssignmentRepository
        // Sletter specifik PersonAssignment på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeletePersonAssignment(int id)
        {
            Console.WriteLine("Server: Delete item called: id = " + id);

            bool deleted = Repository.DeletePersonAssignment(id);
            if (deleted)
            {
                Console.WriteLine("Server: Item deleted succces");
                int code = (int)HttpStatusCode.OK;
                return new StatusCodeResult(code);
            }
            else
            {
                Console.WriteLine("Server: Item deleted fail - not found");
                int code = (int)HttpStatusCode.NotFound;
                return new StatusCodeResult(code);
            }
        }

    }
}
