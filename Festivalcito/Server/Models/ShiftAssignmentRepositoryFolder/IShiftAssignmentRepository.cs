using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models.ShiftAssignmentRepositoryFolder
{
    public interface IShiftAssignmentRepository
    {
        bool CreateShiftAssigned(ShiftAssignment shiftAssigned);
        ShiftAssignment ReadShiftAssigned(int shiftAssignedId);
        List<ShiftAssignment> ReadAllShiftAssigned();
        bool DeleteShiftAssigned(int shiftAssignedId);
    }
}
