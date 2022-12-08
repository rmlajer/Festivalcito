using System;
using Festivalcito.Shared.Models;

namespace Festivalcito.Server.Models.ShiftRepositoryFolder
{
	public interface IShiftRepository
	{
        bool CreateShift(Shift shift);
        Shift ReadShift(int shiftId);
        List<Shift> ReadAllShifts();
        bool UpdateShift(Shift shift);
        bool DeleteShift(int ShiftID);

    } 
}

