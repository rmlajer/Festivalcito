using System;
using System.Net;
using System.Net.Mail;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models{


	public class GlobalConnections{


        public string PGadminConnection =
			"UserID=arvppmkz;Password=PRfiDeTpnyfXNpAqQ221u9tQsx2_RUrV;Host=mouse.db.elephantsql.com;Port=5432;Database=arvppmkz";

        public string mongoConnection = "";

		public void sendMail(Person person){
            Console.WriteLine("Send mail");
            var smtpClient = new SmtpClient("smtp.gmail.com"){
                Port = 587,
                Credentials = new NetworkCredential("mortenlund2608@gmail.com", "nozhog-tatmu5-Kydrip"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("email"),
                Subject = "Test 123",
                Body = "<h1>Hello</h1>",
                IsBodyHtml = true
            };
            mailMessage.To.Add("boes9@icloud.com");

            smtpClient.Send(mailMessage);
        }


	}
}

