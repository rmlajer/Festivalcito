using System;
namespace Festivalcito.Shared.Models
{
	public class Person
	{
		public int PersonID { get; set; }
        public bool Assigned { get; set; }
        public bool IsCoordinator { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Nationality { get; set; }
        public int DanishLevel { get; set; }
        public string? Gender { get; set; }
        public string? MembershipPaid { get; set; }

        public Area? area { get; set; }


        public Person()
		{
		}
	}
}

