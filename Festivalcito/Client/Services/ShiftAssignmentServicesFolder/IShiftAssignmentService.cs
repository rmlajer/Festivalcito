using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.ShiftAssignmentServicesFolder
{
    public interface IShiftAssignmentService
    {
        Task<int> CreateShiftAssignment(ShiftAssignment shiftAssigned);
        Task<ShiftAssignment> ReadShiftAssignment(int ShiftAssignedListId);
        Task<ShiftAssignment[]?> ReadAllShiftAssignments();
        Task<int> DeleteShiftAssignment(int ShiftAssignedListId);
    }
}
