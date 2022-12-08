using System;
using System.Net.Http;
using System.Net.Http.Json;
using Festivalcito.Shared.Models;

namespace Festivalcito.Client.Services.ShiftServicesFolder
{
	public class ShiftService : IShiftService
	{
        private readonly HttpClient httpClient;

        public ShiftService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public Task<bool> CreateShift(Shift shift)
        {
            throw new NotImplementedException();
        }

        public Task<Shift> ReadShift(int shiftId)
        {
            throw new NotImplementedException();
        }

        public Task<Shift[]?> ReadAllShifts()
        {
            var result = httpClient.GetFromJsonAsync<Shift[]>("api/Shift");
            return result;
        }

        public Task<bool> UpdateShift(Shift shift)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteShift(int ShiftID)
        {
            throw new NotImplementedException();
        }



    }
}

