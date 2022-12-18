using System;
using Festivalcito.Server.Models.LoginCredentialRepositoryFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Festivalcito.Server.Controllers{
    /// LoginCredentialController håndterer kald af metoder som kommer fra services og kalder de respektive metoder i AreaRepository
    /// Controlleren fungerer altså som bindeled og tillader os at skifte dele af programmet uden at ødelægge eksisterende struktur.

    [ApiController]
    [Route("api/logincredential")]


    public class LoginCredentialController : ControllerBase
    {
        private readonly ILoginCredentialRepository Repository = new LoginCredentialRepository();


        //contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public LoginCredentialController(ILoginCredentialRepository AssignedRepository)
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
        public void CreateLoginCredential(LoginCredential loginCredential)
        {

            Repository.CreateLoginCredential(loginCredential);
        }

        // Kalder ReadAssigned i tAssignedRepository
        //Den kaldte metode henter specifik Assigned fra DB baseret på SAssignedID
        [HttpGet("{email}")]
        public LoginCredential ReadLoginCredential(string email)
        {
            return Repository.ReadLoginCredential(email);
        }


        //Kalder ReadAllAssigned i AssignedRepository
        // Henter alle Assigned fra DB
        [HttpGet]
        public IEnumerable<LoginCredential> ReadAllLoginCredentials()
        {
            return Repository.ReadAllLoginCredentials();
        }


        // Kalder DeleteAssigned i AssignedRepository
        // Sletter specifik Assigned på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeleteLoginCredential(string email)
        {
            Console.WriteLine("Server: Delete item called: usermail = " + email);

            bool deleted = Repository.DeleteLoginCredential(email);
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

