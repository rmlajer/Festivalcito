using System;
using System.ComponentModel.DataAnnotations;


namespace Festivalcito.Shared.Classes {

    public class Shift{
        public int ShiftID { get; set; }

        [Required]
        public string? ShiftName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RequiredVolunteers { get; set; }
        public int PeopleAssignedToShift { get; set; }
        public int AgeMin { get; set; }
        public float HourMultiplier { get; set; }
        public bool IsLocked { get; set; }

        public int areaId { get; set; }
        public float shiftPoints { get; set; }

        public string backgroundColor = "Green";

        public Shift()
		{
		}

        public void calculateShiftPoints(){
            this.shiftPoints = Convert.ToSingle(Math.Round((EndTime.Subtract(StartTime).TotalHours) * HourMultiplier, 1));
        }

        public float calculateMissingPeople(List<ShiftAssignment> shiftAssignments)
        {
            PeopleAssignedToShift = 0;

            float returnFloat = 0.0f;

            foreach(ShiftAssignment shiftAssignment in shiftAssignments)
            {
                if (shiftAssignment.ShiftId == ShiftID)
                {
                    this.PeopleAssignedToShift++;
                }
            }
            try
            {
                Console.WriteLine("this.PeopleAssignedToShift: " + this.PeopleAssignedToShift);
                Console.WriteLine("RequiredVolunteers: " + RequiredVolunteers);
                returnFloat = (float)this.PeopleAssignedToShift / (float)RequiredVolunteers;
                Console.WriteLine("returnFloat: " + returnFloat);
                Console.WriteLine("");
            }
            catch
            {
                returnFloat = 0.0f;
            }

            Console.WriteLine("ShiftName: " + ShiftName +" returnFloat: " + returnFloat);
            
            return returnFloat;
        }

        public override string ToString()
        {
            return $"ShiftID: {ShiftID}, Name: {ShiftName}, areaName: {areaId}";
        }
    }
}

