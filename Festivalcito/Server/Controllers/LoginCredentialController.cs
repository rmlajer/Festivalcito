using System;
using Festivalcito.Server.Models.LoginCredentialRepositoryFolder;
using Festivalcito.Shared.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Festivalcito.Server.Controllers
{
    /// LoginCredentialController håndterer kald af metoder som kommer fra services og kalder de respektive metoder i AreaRepository
    /// Controlleren fungerer altså som bindeled og tillader os at skifte dele af programmet uden at ødelægge eksisterende struktur.

    [ApiController]
    [Route("api/logincredential")]


    public class LoginCredentialController : ControllerBase
    {
        private readonly ILoginCredentialRepository Repository = new LoginCredentialRepository();


        //Contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public LoginCredentialController(ILoginCredentialRepository credentialRepository)
        {
            if (Repository == null && credentialRepository != null)
            {
                Repository = credentialRepository;

            }
        }

        // Kalder CreateLoginCredential i LoginCredentialRepository
        // Opretter Assigned i DB
        [HttpPost]
        public bool CreateLoginCredential(LoginCredential loginCredential)
        {
            Console.WriteLine("Controller - CreateLoginCredential");
            return Repository.CreateLoginCredential(loginCredential);
        }

        // Kalder ReadLoginCredential i LoginCredentialRepository
        //Den kaldte metode henter specifik LoginCredential fra DB baseret på Email
        [HttpGet("{email}")]
        public LoginCredential ReadLoginCredential(string email)
        {
            Console.WriteLine("Controller - ReadLoginCredential");
            return Repository.ReadLoginCredential(email);
        }


        //Kalder ReadAllLoginCredential i LoginCredentialRepository
        // Henter alle LoginCredential fra DB
        [HttpGet]
        public IEnumerable<LoginCredential> ReadAllLoginCredentials()
        {
            Console.WriteLine("Controller - ReadAllLoginCredentials");
            return Repository.ReadAllLoginCredentials();
        }


        // Kalder DeleteLoginCredential i LoginCredentialRepository
        // Sletter specifik LoginCredential på ID i DB
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeleteLoginCredential(string email)
        {
            Console.WriteLine("Controller - DeleteLoginCredential");
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

