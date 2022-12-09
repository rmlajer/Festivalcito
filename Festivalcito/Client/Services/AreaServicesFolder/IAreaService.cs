using System;
using Festivalcito.Shared.Models;

namespace Festivalcito.Client.Services.AreaServicesFolder
{
	public interface IAreaService
	{
        Task<int> CreateArea(Area person);
        Task<Shift> ReadArea(int AreaId);
        Task<Shift[]?> ReadAllAreas();
        Task<int> UpdateArea(Area person);
        Task<int> DeleteArea(int AreaID);
    }
}

