using System;

namespace Festivalcito.Shared.Classes {

    public class Shift{
        public int ShiftID { get; set; }
        public string? ShiftName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RequiredVolunteers { get; set; }
        public int AgeMin { get; set; }
        public float HourMultiplier { get; set; }
        public bool IsLocked { get; set; }

        public string? areaName { get; set; }
        public float? shiftPoints { get; set; }

        public string backgroundColor = "Green";

        public Shift()
		{
		}

        public void calculateShiftPoints(){
            this.shiftPoints = Convert.ToSingle(Math.Round((EndTime.Subtract(StartTime).TotalHours) * HourMultiplier, 1));
        }

        public void assignCurrentShiftToPerson()
        {
            
        }

        public override string ToString()
        {
            return $"ShiftID: {ShiftID}, Name: {ShiftName}, areaName: {areaName}";
        }
    }
}

