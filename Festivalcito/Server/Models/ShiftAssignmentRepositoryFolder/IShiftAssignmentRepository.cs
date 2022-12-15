using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models.ShiftAssignmentRepositoryFolder
{
    public interface IShiftAssignmentRepository
    {
        bool CreateShiftAssignment(ShiftAssignment shiftAssigned);
        ShiftAssignment ReadShiftAssignment(int shiftAssignedId);
        List<ShiftAssignment> ReadAllShiftAssignments();
        bool DeleteShiftAssignment(int shiftAssignedId);
    }
}
