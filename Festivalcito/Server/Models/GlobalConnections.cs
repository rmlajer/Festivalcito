using System;
using System.Net;
using System.Net.Mail;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models
{


    public class GlobalConnections
    {

        //Backup DB
        //public string PGadminConnection =
        //"UserID=arvppmkz;Password=PRfiDeTpnyfXNpAqQ221u9tQsx2_RUrV;Host=mouse.db.elephantsql.com;Port=5432;Database=arvppmkz";

        public string AzureConnection = "Server=festivalcito.postgres.database.azure.com;" +
            "Database=postgres;" +
            "Port=5432;" +
            "User Id=admincito;" +
            "Password=Bezos123!;";

        public void SendMail(Person person)
        {
            Console.WriteLine("GlobalConnections - Send mail");
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("festivalcitodk@gmail.com", "snrpqscxioxvggqf"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("festivalcitodk@gmail.com"),
                Subject = "Become a volunteer once again :-)",
                Body = $"<span>Hi {person.FirstName} {person.LastName}</span><br>" +
                $"<span>We hope you would like to become a volunteer at our Festivalcito in 2023</span>",
                IsBodyHtml = true
            };
            mailMessage.To.Add($"{person.EmailAddress}");

            smtpClient.Send(mailMessage);
        }


    }
}

