using System;
using Festivalcito.Shared.Classes;

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

