using System;
namespace Festivalcito.Shared.Models
{
	public class LoginCredentials
	{
		public int LoginCredentialID { get; set; }
        public string? UserEmail { get; set; }
		public string? HashedPassword { get; set; }


        public LoginCredentials()
		{
		}
	}
}

