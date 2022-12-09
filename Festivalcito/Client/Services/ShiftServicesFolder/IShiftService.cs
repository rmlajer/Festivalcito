using System;
using Festivalcito.Shared.Models;
namespace Festivalcito.Client.Services.ShiftServicesFolder{

	public interface IShiftService{
        Task<int> CreateShift(Shift shift);
        Task<Shift> ReadShift(int shiftId);
        Task<Shift[]?> ReadAllShifts();
        Task<int> UpdateShift(Shift shift);
        Task<int> DeleteShift(int ShiftID);
    }
}

