﻿using System;
using Festivalcito.Shared.Classes;
using Npgsql;
using Dapper;

namespace Festivalcito.Server.Models.LoginCredentialRepositoryFolder
{
	public class LoginCredentialRepository : GlobalConnections, ILoginCredentialRepository
	{
		public LoginCredentialRepository()
		{
		}

        public bool CreateLoginCredential(LoginCredential loginCredential)
        {
            //Tager et assigned Assigned object og indsætter via SQL statement i vores database
            Console.WriteLine("CreateLoginCredential - Repository");
            var sql = $"INSERT INTO logincredential (useremail, hashedpassword) VALUES ('{loginCredential.UserEmail}', '{loginCredential.HashedPassword}')";
            Console.WriteLine("sql: " + sql);

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                try
                {
                    connection.Execute(sql);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Couldn't add the assigned to the list: " + e.Message);
                    return false;
                }
            }
        }
        public LoginCredential ReadLoginCredential(string email)
        {
            Console.WriteLine("ReadLoginCredential - Repository");
            var SQL = $"SELECT * FROM logincredential WHERE useremail ilike '{email}'";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            LoginCredential returnAssigned = new LoginCredential();
            try
            {
                using (var connection = new NpgsqlConnection(PGadminConnection))
                {
                    returnAssigned = connection.Query<LoginCredential>(SQL).First();
                    return returnAssigned;
                }
            }catch
            {
                Console.WriteLine("No match on email");
                return new LoginCredential();
            }
            
        }
        public List<LoginCredential> ReadAllLoginCredentials()
        {
            Console.WriteLine("ReadAllLoginCredentials - Repository");
            var SQL = $"SELECT * FROM logincredential";
            List<LoginCredential> returnList = new List<LoginCredential>();

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnList = connection.Query<LoginCredential>(SQL).ToList();

            }

            return returnList;
        }
        public bool DeleteLoginCredential(string email)
        {
            Console.WriteLine("DeleteLoginCredential - Repository");
            var sql = $"DELETE FROM public.logincredential WHERE useremail ilike {email}";

            Console.WriteLine(sql);
            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                try
                {
                    connection.Execute(sql);
                    return true;
                }
                catch
                {
                    Console.WriteLine("Couldn't delete the LoginCredential in the list");
                    return false;
                }
            }
        }
    }
}
