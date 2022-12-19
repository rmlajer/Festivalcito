using System;
using Festivalcito.Shared.Classes;
using Npgsql;
using Dapper;

namespace Festivalcito.Server.Models.LoginCredentialRepositoryFolder{

	public class LoginCredentialRepository : GlobalConnections, ILoginCredentialRepository{
		public LoginCredentialRepository()
		{
		}

        public bool CreateLoginCredential(LoginCredential loginCredential){
            //Tager et assigned Assigned object og indsætter via SQL statement i vores database
            Console.WriteLine("Repository - CreateLoginCredential");
            var sql = $"INSERT INTO logincredential (useremail, hashedpassword) VALUES ('{loginCredential.UserEmail!.ToLower()}', '{loginCredential.HashedPassword}')";
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
                    Console.WriteLine("Couldn't add the login to the list: " + e.Message);
                    return false;
                }
            }
        }
        public LoginCredential ReadLoginCredential(string email){
            Console.WriteLine("Repository - ReadLoginCredential");
            var SQL = $"SELECT * FROM logincredential WHERE useremail ilike '{email}'";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            Console.WriteLine(SQL);
            LoginCredential returnLoginCredential = new LoginCredential();
            try{
                using (var connection = new NpgsqlConnection(PGadminConnection)){
                    var tmpLogin = connection.Query<LoginCredential>(SQL).First();
                    tmpLogin.loginResponse = "Login sucessfull";

                    return tmpLogin;
                }
            }catch (Exception e){
                Console.WriteLine("No match on email: " + e.Message);
                return new LoginCredential(message:e.Message);
            }
            
        }
        public List<LoginCredential> ReadAllLoginCredentials(){
            Console.WriteLine("Repository - ReadAllLoginCredentials");
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
        public bool DeleteLoginCredential(string email){
            Console.WriteLine("Repository - ReadAllLoginCredentials");
            var sql = $"DELETE FROM public.DeleteLoginCredential WHERE useremail ilike {email}";

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

