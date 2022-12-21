using System;
using Festivalcito.Shared.Classes;
using System.Net.Http.Json;

namespace Festivalcito.Client.Services.AreaServicesFolder
{
    public class AreaService : IAreaService
    {
        private readonly HttpClient httpClient;

        public AreaService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<int> CreateArea(Area area)
        {
            var response = await httpClient.PostAsJsonAsync<Area>("api/area/", area);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<Area> ReadArea(int? areaId)
        {
            return (await httpClient.GetFromJsonAsync<Area>("api/area/" + areaId!))!;
        }

        public async Task<Area[]?> ReadAllAreas()
        {
            return await httpClient.GetFromJsonAsync<Area[]>("api/area/");
        }

        public async Task<int> UpdateArea(Area area)
        {
            var response = await httpClient.PutAsJsonAsync<Area>("api/area/", area);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }

        public async Task<int> DeleteArea(int areaID)
        {
            var response = await httpClient.DeleteAsync("api/area/" + areaID);
            var responseStatusCode = response.StatusCode;
            return (int)responseStatusCode;
        }
    }
}

