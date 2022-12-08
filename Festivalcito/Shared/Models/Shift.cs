using System;
namespace Festivalcito.Shared.Models
{
	public class Shift
	{
        public int ShiftID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RequiredVolunteers { get; set; }
        public int AgeMin { get; set; }
        public float HourMultiplier { get; set; }


        public Shift()
		{
		}
	}
}

