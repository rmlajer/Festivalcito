using System;
namespace Festivalcito.Shared.Classes{
	public class LoginCredential{
		public int LoginCredentialID { get; set; }
        public string UserEmail { get; set; }
		public string HashedPassword { get; set; }


        public LoginCredential(){
            this.UserEmail = "";
            this.HashedPassword = "";
		}

        

    }
}

