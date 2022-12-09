using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Festivalcito.Server.Models.ShiftRepositoryFolder;
using Festivalcito.Server.Models;
using Festivalcito.Shared.Models;

namespace Festivalcito.Server.Controllers{


    /// ShiftController håndterer kald af metoder som kommer fra services og kalder de respektive metoder i ShiftRepository
    /// Controlleren fungerer altså som bindeled og tillader os at skifte dele af programmet uden at ødelægge eksisterende struktur.

    [ApiController]
    [Route("api/Shift")]


    public class ShiftController : ControllerBase
    {
        private readonly IShiftRepository Repository = new ShiftRepository();


        //contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public ShiftController(IShiftRepository shelterRepository)
        {
            if (Repository == null && shelterRepository != null)
            {
                Repository = shelterRepository;
                Console.WriteLine("Repository initialized");
            }
        }

        // Kalder CreateShift i ShiftRepository
        // Opretter Shift i DB
        [HttpPost]
        public void CreateShift(Shift item)
        {
            Repository.CreateShift(item);
        }

        // Kalder ReadShift i ShiftRepository
        //Den kaldte metode henter specifik Shift fra DB baseret på ShiftID
        [HttpGet("{id:int}")]
        public Shift ReadShift(int id)
        {
            return Repository.ReadShift(id);
        }


        //Kalder ReadAllShift i ShiftRepository
        // Henter alle Shifts fra DB
        [HttpGet]
        public IEnumerable<Shift> ReadAllShifts()
        {
            return Repository.ReadAllShifts();
        }

        // Kalder UpdateShift i ShiftRepository
        // Opdaterer Shift i DB - Alle kolonner i en række bliver opdateret
        [HttpPut]
        public void UpdateShift(Shift shift)
        {
            Repository.UpdateShift(shift);
        }


        // Kalder DeleteShift i ShiftRepository
        // Sletter specifik Shift på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeleteShift(int id)
        {
            Console.WriteLine("Server: Delete item called: id = " + id);

            bool deleted = Repository.DeleteShift(ShiftID: id);
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

