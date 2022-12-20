using System;
using System.ComponentModel.DataAnnotations;


namespace Festivalcito.Shared.Classes {

    public class Shift{

        public int ShiftID { get; set; }

        [Required]
        [StringLength(50)]
        public string? ShiftName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/5/2023", "4/5/2023", ErrorMessage = "Date is out of Range [01/05 - 03/05]")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/5/2023", "4/5/2023", ErrorMessage = "Date is out of Range [01/05 - 03/05]")]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1,100, ErrorMessage = "You can't assign more than 100 volunteers to one shift")]
        public int RequiredVolunteers { get; set; }

        [Required]
        public int PeopleAssignedToShift { get; set; }

        [Required]
        [Range(16, 100, ErrorMessage = "Minimum age is 16")]
        public int AgeMin { get; set; }

        [Required]
        public float HourMultiplier { get; set; }

        public bool IsLocked { get; set; }

        public int areaId { get; set; }

        public float shiftPoints { get; set; }

        public string backgroundColor = "Green";

        public Shift()
		{
            this.RequiredVolunteers = 1;
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
                returnFloat = (float)this.PeopleAssignedToShift / (float)RequiredVolunteers;
            }
            catch
            {
                returnFloat = 0.0f;
            }
            
            return returnFloat;
        }

        public override string ToString()
        {
            return $"ShiftID: {ShiftID}, Name: {ShiftName}, areaName: {areaId}";
        }
    }
}

