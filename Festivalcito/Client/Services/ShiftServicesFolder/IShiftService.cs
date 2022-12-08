using System;
using Festivalcito.Shared.Models;
namespace Festivalcito.Client.Services.ShiftServicesFolder
{
	public interface IShiftService
	{
     
        Task<bool> CreateShift(Shift shift);
        Task<Shift> ReadShift(int shiftId);
        Task<Shift[]?> ReadAllShifts();
        Task<bool> UpdateShift(Shift shift);
        Task <bool> DeleteShift(int ShiftID);

    }
}

