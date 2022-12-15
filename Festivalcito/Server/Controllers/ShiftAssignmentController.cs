using Festivalcito.Server.Models.ShiftAssignmentRepositoryFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Festivalcito.Server.Controllers
{
    /// AreaController håndterer kald af metoder som kommer fra services og kalder de respektive metoder i AreaRepository
    /// Controlleren fungerer altså som bindeled og tillader os at skifte dele af programmet uden at ødelægge eksisterende struktur.

    [ApiController]
    [Route("api/shiftassigned")]


    public class ShiftAssignmentController : ControllerBase
    {
        private readonly IShiftAssignmentRepository Repository = new ShiftAssignmentRepository();


        //contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public ShiftAssignmentController(IShiftAssignmentRepository shiftAssignedRepository)
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
        public void CreateArea(ShiftAssignment shiftAssigned)
        {

            Repository.CreateShiftAssignment(shiftAssigned);
        }

        // Kalder ReadShiftAssigned i ShiftAssignedRepository
        //Den kaldte metode henter specifik ShiftAssigned fra DB baseret på ShiftAssignedID
        [HttpGet("{id:int}")]
        public ShiftAssignment ReadShiftAssigned(int id)
        {
            return Repository.ReadShiftAssignment(id);
        }


        //Kalder ReadAllShiftAssigned i ShiftAssignedRepository
        // Henter alle ShiftAssigned fra DB
        [HttpGet]
        public IEnumerable<ShiftAssignment> ReadAllShiftAssigned()
        {
            return Repository.ReadAllShiftAssignments();
        }


        // Kalder DeleteShiftAssigned i ShiftAssignedRepository
        // Sletter specifik ShiftAssigned på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeleteShiftAssigned(int id)
        {
            Console.WriteLine("Server: Delete item called: id = " + id);

            bool deleted = Repository.DeleteShiftAssignment(id);
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
