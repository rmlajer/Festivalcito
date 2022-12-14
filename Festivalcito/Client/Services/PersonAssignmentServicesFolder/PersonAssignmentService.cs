using System;
using Festivalcito.Shared.Classes;
using System.Net.Http.Json;

namespace Festivalcito.Client.Services.PersonAssignmentServicesFolder
{
    public class PersonAssignmentService : IPersonAssignmentService
    {
        private readonly HttpClient httpClient;

        public PersonAssignmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<int> CreateAssigned(PersonAssignment Assigned)
        {
            var response = await httpClient.PostAsJsonAsync<PersonAssignment>("api/assigned/", Assigned);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<PersonAssignment> ReadAssigned(int AssignedListId)
        {
            return (await httpClient.GetFromJsonAsync<PersonAssignment>("api/assigned/" + AssignedListId))!;
        }

        public async Task<PersonAssignment[]?> ReadAllAssigned()
        {
            return await httpClient.GetFromJsonAsync<PersonAssignment[]>("api/assigned/");
        }


        public async Task<int> DeleteAssigned(int AssignedListId)
        {
            var response = await httpClient.DeleteAsync("api/assigned/" + AssignedListId);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }



    }
}

