using System;
using System.ComponentModel.DataAnnotations;

namespace Festivalcito.Shared.Classes{
	public class Person{

		public int PersonID { get; set; }
        public bool IsCoordinator { get; set; }
        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? Nationality { get; set; }
        [Required]
        public int DanishLevel { get; set; }
        [Required]
        public string? Gender { get; set; }

        public bool MembershipPaid { get; set; }

        public int areaId { get; set; }

        public string personCreateResponse = "";

        public Person()
		{
		}

        


        public override string ToString()
        {
            return $"{FirstName}, {LastName}, {IsCoordinator}, {EmailAddress}, {DateOfBirth}, {Address}, {PostalCode}, {City}, {Country}, {Nationality}, {DanishLevel}, {Gender}, {MembershipPaid}, {areaId}";
        }
    }
}

