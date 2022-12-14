using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.ShiftAssignmentServicesFolder
{
    public interface IShiftAssignmentService
    {
        Task<int> CreateShiftAssigned(ShiftAssignment shiftAssigned);
        Task<ShiftAssignment> ReadShiftAssigned(int ShiftAssignedListId);
        Task<ShiftAssignment[]?> ReadAllShiftAssigned();
        Task<int> DeleteShiftAssigned(int ShiftAssignedListId);
    }
}
