using System;
using Festivalcito.Shared.Models;
using Dapper;
using Npgsql;

namespace Festivalcito.Server.Models.PersonRepositoryFolder{
    public class PersonRepository : GlobalConnections, IPersonRepository {

        public PersonRepository()
        {
        }    
        

        public bool CreatePerson(Person person)
        {
            //Tager et Person object og indsætter via SQL statement i vores database
            //Formatere DateOfBirth for at det passer med postgreSQL 
            Console.WriteLine("CreatePerson - Repository");
            var sql = $"INSERT INTO public.personlist" +
                $"(assigned, iscoordinator, emailadress, firstname, lastname, dateofbirth," +
                $"address, postalcode, city, country, nationality, danishlevel, gender, membershippaid)" +
                $"VALUES (" +
                $"  {person.Assigned}," +
                $"  {person.IsCoordinator}," +
                $" '{person.EmailAddress}', " +
                $" '{person.FirstName}'," +
                $" '{person.LastName}'," +
                $" '{person.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss").Replace("\"", "").Replace(".", ":")}', " +
                $" '{person.Address}'," +
                $" '{person.PostalCode}', " +
                $" '{person.City}', " +
                $" '{person.Country}', " +
                $" '{person.Nationality}'," +
                $"  {person.DanishLevel}, " +
                $" '{person.Gender}', " +
                $" {person.MembershipPaid})";

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
                    Console.WriteLine("Couldn't add the Person to the list: " + e.Message);
                    return false;
                }
            }
        }



        //Læser et Person objekt fra databasen ved brug af personID
        public Person ReadPerson(int personId)
        {
            Console.WriteLine("ReadPerson");
            var SQL = $"SELECT * from public.personlist WHERE personID = {personId}";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            Person returnPerson = new Person();
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnPerson = connection.Query<Person>(SQL).First();
                return returnPerson;
            }
        }


        //Læser alle Persons objekter fra databasen
        public List<Person> ReadAllPersons()
        {
            Console.WriteLine("ReadAllPersons");
            var SQL = "SELECT  * FROM public.personlist;";
            List<Person> returnList = new List<Person>();

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnList = connection.Query<Person>(SQL).ToList();

            }

            return returnList;
        }



        //Overskriver et entry i tablen Person med det nye objeckt Person af klassen Person
        public bool UpdatePerson(Person person)
        {

            Console.WriteLine("UpdatePerson");
            var sql = $"UPDATE public.personlist SET " +
                $"assigned= {person.Assigned}, " +
                $"iscoordinator = {person.IsCoordinator}, " +
                $"emailaddress = '{person.EmailAddress}', " +
                $"firstname = '{person.FirstName}', " +
                $"lastname = '{person.LastName}', " +
                $"dateofbirth ='{person.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss").Replace("\"", "").Replace(".", ":")}',  " +
                $"address = '{person.Address}', " +
                $"postalcode = '{person.PostalCode}', " +
                $"city ='{person.City}', " +
                $"country ='{person.Country}', " +
                $"nationality ='{person.Nationality}', " +
                $"danishlevel ={person.DanishLevel}, " +
                $"gender ='{person.Gender}', " +
                $"membershippaid ={person.MembershipPaid} " +
                $"WHERE personId = {person.PersonID}"; 


      
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
                    Console.WriteLine("Couldn't update the Person in the list: " + e.Message);
                    return false;
                }
            }

        }

        public bool DeletePerson(int personId)
        {

            Console.WriteLine("DeletePerson");
            var sql = $"DELETE FROM public.personlist WHERE personid = {personId}";

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
                    Console.WriteLine("Couldn't Delete the Person from the list");
                    return false;
                }

            }
        }
    }
        
}

