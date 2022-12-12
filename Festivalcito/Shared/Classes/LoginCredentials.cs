using System;
namespace Festivalcito.Shared.Classes{
	public class LoginCredentials{
		public int LoginCredentialID { get; set; }
        public string? UserEmail { get; set; }
		public string? HashedPassword { get; set; }


        public LoginCredentials()
		{
		}
	}
}

