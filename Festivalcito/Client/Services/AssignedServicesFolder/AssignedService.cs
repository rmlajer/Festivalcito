using System;
using Festivalcito.Shared.Models;
using System.Net.Http.Json;

namespace Festivalcito.Client.Services.AssignedServicesFolder
{
    public class AssignedService : IAssignedService
    {
        private readonly HttpClient httpClient;

        public AssignedService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<int> CreateAssigned(Assigned Assigned)
        {
            var response = await httpClient.PostAsJsonAsync<Assigned>("api/assigned/", Assigned);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<Assigned> ReadAssigned(int AssignedListId)
        {
            return (await httpClient.GetFromJsonAsync<Assigned>("api/assigned/" + AssignedListId))!;
        }

        public async Task<Assigned[]?> ReadAllAssigned()
        {
            return await httpClient.GetFromJsonAsync<Assigned[]>("api/assigned/");
        }


        public async Task<int> DeleteAssigned(int AssignedListId)
        {
            var response = await httpClient.DeleteAsync("api/assigned/" + AssignedListId);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }



    }
}

