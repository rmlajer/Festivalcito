using System;
using System.Net.Http;
using System.Net.Http.Json;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.PersonServicesFolder{
	public class PersonService : IPersonService{

        private readonly HttpClient httpClient;

        public PersonService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> CreatePerson(Person person) {
            var response = await httpClient.PostAsJsonAsync<Person>("api/person", person);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }
        public async Task<Person> ReadPerson(int personId)
        {
            return (await httpClient.GetFromJsonAsync<Person>("api/person/nojoin/" + personId))!;
        }

        public async Task<Person> ReadPersonJoinArea(int personId)
        {
            return (await httpClient.GetFromJsonAsync<Person>("api/person/yesjoin/" + personId))!;
        }

        public async Task<Person[]?> ReadAllPersons()
        {
            return await httpClient.GetFromJsonAsync<Person[]>("api/person");
        }
        public async Task<int> UpdatePerson(Person person)
        {
            var response = await httpClient.PutAsJsonAsync<Person>("api/person", person);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }
        public async Task<int> DeletePerson(int personID)
        {
            var response = await httpClient.DeleteAsync("api/person" + personID);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }
    }
}

