using Festivalcito.Server.Models.ShiftAssignedRepositoryFolder;
using Festivalcito.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Festivalcito.Server.Controllers
{
    /// AreaController håndterer kald af metoder som kommer fra services og kalder de respektive metoder i AreaRepository
    /// Controlleren fungerer altså som bindeled og tillader os at skifte dele af programmet uden at ødelægge eksisterende struktur.

    [ApiController]
    [Route("api/shiftassigned")]


    public class ShiftAssignedController : ControllerBase
    {
        private readonly IShiftAssignedRepository Repository = new ShiftAssignedRepository();


        //contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public ShiftAssignedController(IShiftAssignedRepository shiftAssignedRepository)
        {
            if (Repository == null && shiftAssignedRepository != null)
            {
                Repository = shiftAssignedRepository;
                Console.WriteLine("Repository initialized");
            }
        }

        // Kalder CreateShiftAssigned i ShiftAssignedRepository
        // Opretter ShiftAssigned i DB
        [HttpPost]
        public void CreateArea(ShiftAssigned shiftAssigned)
        {

            Repository.CreateShiftAssigned(shiftAssigned);
        }

        // Kalder ReadShiftAssigned i ShiftAssignedRepository
        //Den kaldte metode henter specifik ShiftAssigned fra DB baseret på ShiftAssignedID
        [HttpGet("{id:int}")]
        public ShiftAssigned ReadShiftAssigned(int id)
        {
            return Repository.ReadShiftAssigned(id);
        }


        //Kalder ReadAllShiftAssigned i ShiftAssignedRepository
        // Henter alle ShiftAssigned fra DB
        [HttpGet]
        public IEnumerable<ShiftAssigned> ReadAllShiftAssigned()
        {
            return Repository.ReadAllShiftAssigned();
        }


        // Kalder DeleteShiftAssigned i ShiftAssignedRepository
        // Sletter specifik ShiftAssigned på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeleteShiftAssigned(int id)
        {
            Console.WriteLine("Server: Delete item called: id = " + id);

            bool deleted = Repository.DeleteShiftAssigned(id);
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
