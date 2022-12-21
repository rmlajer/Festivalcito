using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models.AreaRepositoryFolder
{
    public interface IAreaRepository
    {
        bool CreateArea(Area area);
        Area ReadArea(int areaId);
        List<Area> ReadAllAreas();
        bool UpdateArea(Area area);
        bool DeleteArea(int areaId);
    }
}

