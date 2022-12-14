using Festivalcito.Client.Services.ShiftAssignmentServicesFolder;
using Festivalcito.Shared.Classes;
using System.Net.Http.Json;

namespace Festivalcito.Client.Services.ShiftAssignmentServicesFolder
{
    public class ShiftAssignmentService : IShiftAssignmentService
    {

        private readonly HttpClient httpClient;

        public ShiftAssignmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<int> CreateShiftAssigned(ShiftAssignment shiftAssigned)
        {
            var response = await httpClient.PostAsJsonAsync<ShiftAssignment>("api/shiftassigned/", shiftAssigned);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<ShiftAssignment> ReadShiftAssigned(int shiftAssignedListId)
        {
            return (await httpClient.GetFromJsonAsync<ShiftAssignment>("api/shiftassigned/" + shiftAssignedListId))!;
        }

        public async Task<ShiftAssignment[]?> ReadAllShiftAssigned()
        {
            return await httpClient.GetFromJsonAsync<ShiftAssignment[]>("api/shiftassigned/");
        }


        public async Task<int> DeleteShiftAssigned(int shiftAssignedListId)
        {
            var response = await httpClient.DeleteAsync("api/shiftassigned/" + shiftAssignedListId);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }



    }
}

