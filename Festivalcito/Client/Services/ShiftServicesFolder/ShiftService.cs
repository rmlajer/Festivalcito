using System;
using System.Net.Http;
using System.Net.Http.Json;
using Festivalcito.Shared.Models;

namespace Festivalcito.Client.Services.ShiftServicesFolder{
	public class ShiftService : IShiftService{

        private readonly HttpClient httpClient;

        public ShiftService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<int> CreateShift(Shift shift)
        {
            var response = await httpClient.PostAsJsonAsync<Shift>("api/Shift", shift);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<Shift> ReadShift(int shiftId)
        {
            return (await httpClient.GetFromJsonAsync<Shift>("api/Shift/" + shiftId))!;
        }

        public async Task<Shift[]?> ReadAllShifts()
        {
            return await httpClient.GetFromJsonAsync<Shift[]>("api/Shift");
        }

        public async Task<int> UpdateShift(Shift shift)
        {
            var response = await httpClient.PutAsJsonAsync<Shift>("api/Shift", shift);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<int> DeleteShift(int ShiftID)
        {
            throw new NotImplementedException();
        }



    }
}

