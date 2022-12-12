using Festivalcito.Client.Services.ShiftAssignedServicesFolder;
using Festivalcito.Shared.Classes;
using System.Net.Http.Json;

namespace Festivalcito.Client.Services.ShiftAssignedServicesFolder
{
    public class ShiftAssignedService : IShiftAssignedService
    {

        private readonly HttpClient httpClient;

        public ShiftAssignedService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<int> CreateShiftAssigned(ShiftAssigned shiftAssigned)
        {
            var response = await httpClient.PostAsJsonAsync<ShiftAssigned>("api/shiftassigned/", shiftAssigned);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<ShiftAssigned> ReadShiftAssigned(int shiftAssignedListId)
        {
            return (await httpClient.GetFromJsonAsync<ShiftAssigned>("api/shiftassigned/" + shiftAssignedListId))!;
        }

        public async Task<ShiftAssigned[]?> ReadAllShiftAssigned()
        {
            return await httpClient.GetFromJsonAsync<ShiftAssigned[]>("api/shiftassigned/");
        }


        public async Task<int> DeleteShiftAssigned(int shiftAssignedListId)
        {
            var response = await httpClient.DeleteAsync("api/shiftassigned/" + shiftAssignedListId);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }



    }
}

