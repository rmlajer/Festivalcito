using System;
using Festivalcito.Server.Models.AreaRepositoryFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Festivalcito.Server.Controllers
{
    /// AreaController håndterer kald af metoder som kommer fra services og kalder de respektive metoder i AreaRepository
    /// Controlleren fungerer altså som bindeled og tillader os at skifte dele af programmet uden at ødelægge eksisterende struktur.

    [ApiController]
    [Route("api/area")]


    public class AreaController : ControllerBase{
        private readonly IAreaRepository Repository = new AreaRepository();


        //contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public AreaController(IAreaRepository areaRepository){
            if (Repository == null && areaRepository != null)
            {
                Repository = areaRepository;
                Console.WriteLine("Repository initialized");
            }
        }

        // Kalder CreateArea i AreaRepository
        // Opretter Area i DB
        [HttpPost]
        public void CreateArea(Area Area){
            
            Repository.CreateArea(Area);
        }

        // Kalder ReadArea i AreaRepository
        //Den kaldte metode henter specifik Area fra DB baseret på AreaID
        [HttpGet("{id:int}")]
        public Area ReadArea(int id){
            return Repository.ReadArea(id);
        }


        //Kalder ReadAllArea i AreaRepository
        // Henter alle Areas fra DB
        [HttpGet]
        public IEnumerable<Area> ReadAllAreas(){
            return Repository.ReadAllAreas();
        }

        // Kalder UpdateArea i AreaRepository
        // Opdaterer Area i DB - Alle kolonner i en række bliver opdateret
        [HttpPut]
        public void UpdateArea(Area Area)
        {
            Repository.UpdateArea(Area);
        }


        // Kalder DeleteArea i AreaRepository
        // Sletter specifik Area på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeleteArea(int id)
        {
            Console.WriteLine("Server: Delete item called: id = " + id);

            bool deleted = Repository.DeleteArea(id);
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

