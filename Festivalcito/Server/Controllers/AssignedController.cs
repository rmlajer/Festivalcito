using Festivalcito.Server.Models.AssignedRepositoryFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Festivalcito.Server.Controllers
{
    /// AreaController håndterer kald af metoder som kommer fra services og kalder de respektive metoder i AreaRepository
    /// Controlleren fungerer altså som bindeled og tillader os at skifte dele af programmet uden at ødelægge eksisterende struktur.

    [ApiController]
    [Route("api/assigned")]


    public class AssignedController : ControllerBase
    {
        private readonly IAssignedRepository Repository = new AssignedRepository();


        //contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public AssignedController(IAssignedRepository AssignedRepository)
        {
            if (Repository == null && AssignedRepository != null)
            {
                Repository = AssignedRepository;
                Console.WriteLine("Repository initialized");
            }
        }

        // Kalder CreateAssigned i AssignedRepository
        // Opretter Assigned i DB
        [HttpPost]
        public void CreateArea(Assigned Assigned)
        {

            Repository.CreateAssigned(Assigned);
        }

        // Kalder ReadAssigned i tAssignedRepository
        //Den kaldte metode henter specifik Assigned fra DB baseret på SAssignedID
        [HttpGet("{id:int}")]
        public Assigned ReadAssigned(int id)
        {
            return Repository.ReadAssigned(id);
        }


        //Kalder ReadAllAssigned i AssignedRepository
        // Henter alle Assigned fra DB
        [HttpGet]
        public IEnumerable<Assigned> ReadAllAssigned()
        {
            return Repository.ReadAllAssigned();
        }


        // Kalder DeleteAssigned i AssignedRepository
        // Sletter specifik Assigned på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeleteAssigned(int id)
        {
            Console.WriteLine("Server: Delete item called: id = " + id);

            bool deleted = Repository.DeleteAssigned(id);
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
