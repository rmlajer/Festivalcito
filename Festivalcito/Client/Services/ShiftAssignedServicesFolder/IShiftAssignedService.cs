using Festivalcito.Shared.Models;

namespace Festivalcito.Client.Services.ShiftAssignedServicesFolder
{
    public interface IShiftAssignedService
    {
        Task<int> CreateShiftAssigned(ShiftAssigned shiftAssigned);
        Task<ShiftAssigned> ReadShiftAssigned(int ShiftAssignedListId);
        Task<ShiftAssigned[]?> ReadAllShiftAssigned();
        Task<int> DeleteShiftAssigned(int ShiftAssignedListId);
    }
}
