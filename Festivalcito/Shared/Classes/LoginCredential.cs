using System;
using System.ComponentModel.DataAnnotations;

namespace Festivalcito.Shared.Classes
{
    public class LoginCredential
    {
        public int LoginCredentialID { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        public string HashedPassword { get; set; }

        public string loginResponse { get; set; }


        public LoginCredential()
        {
            this.UserEmail = "";
            this.HashedPassword = "";
            this.loginResponse = "";
        }
        public LoginCredential(string message)
        {
            this.UserEmail = "";
            this.HashedPassword = "";
            this.loginResponse = message;
        }



    }
}

