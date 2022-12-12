using System;
using Festivalcito.Shared.Classes;

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
