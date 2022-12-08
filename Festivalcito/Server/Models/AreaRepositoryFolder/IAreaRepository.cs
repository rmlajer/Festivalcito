using System;
using Festivalcito.Shared.Models;

namespace Festivalcito.Server.Models.AreaRepositoryFolder
{
	public interface IAreaRepository{
        void CreateArea(Area area);
        void ReadArea(Area area);
        void UpdateArea(Area area);
        void DeleteArea(Area area);
    }
}

