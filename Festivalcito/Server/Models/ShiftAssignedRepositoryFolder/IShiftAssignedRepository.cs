using System;
using Festivalcito.Shared.Models;

namespace Festivalcito.Server.Models.ShiftAssignedRepositoryFolder
{
    public interface IShiftAssignedRepository
    {
        bool CreateShiftAssigned(ShiftAssigned shiftAssigned);
        ShiftAssigned ReadShiftAssigned(int shiftAssignedId);
        List<ShiftAssigned> ReadAllShiftAssigned();
        bool DeleteShiftAssigned(int shiftAssignedId);
    }
}
