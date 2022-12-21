using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.AreaServicesFolder
{
    public interface IAreaService
    {
        Task<int> CreateArea(Area person);
        Task<Area> ReadArea(int? AreaId);
        Task<Area[]?> ReadAllAreas();
        Task<int> UpdateArea(Area person);
        Task<int> DeleteArea(int AreaID);
    }
}

