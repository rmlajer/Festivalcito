using System;
using Festivalcito.Shared.Classes;
using Dapper;
using Npgsql;

namespace Festivalcito.Server.Models.PersonRepositoryFolder{
    public class PersonRepository : GlobalConnections, IPersonRepository {

        public PersonRepository()
        {
        }    
        

        public bool CreatePerson(Person person){
            //Tager et Person object og indsætter via SQL statement i vores database
            //Formatere DateOfBirth for at det passer med postgreSQL 
            Console.WriteLine("CreatePerson - Repository");
            var sql = $"INSERT INTO public.person" +
                $"(iscoordinator, emailaddress, firstname, lastname, dateofbirth," +
                $"address, postalcode, city, country, nationality, danishlevel, gender, membershippaid, phonenumber)" +
                $"VALUES (" +
                $"  false," +
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
                $" false, " +
                $" '{person.PhoneNumber}')";

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
        public Person ReadPerson(int personId){
            Console.WriteLine("ReadPerson");
            var SQL = $"SELECT * from public.person WHERE personID = {personId}";
            Console.WriteLine(SQL);
            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            Person returnPerson = new Person();
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnPerson = connection.Query<Person>(SQL).First();
                return returnPerson;
            }
        }

        public Person ReadPersonEmail(string email)
        {
            Console.WriteLine("Repository - ReadPerson");
            var SQLJoinArea = $"SELECT person.personid, iscoordinator, emailaddress, firstname, lastname," +
                $"dateofbirth, address, postalcode, city, country, nationality, danishlevel, gender, membershippaid," +
                $"phonenumber, areaid " +
                $"FROM public.person " +
                $"LEFT JOIN public.personassignment on personassignment.personid = person.personid " +
                $"where person.emailaddress ilike '{email}'";
            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            Console.WriteLine(SQLJoinArea);
            Person returnPerson = new Person();
            try
            {
                using (var connection = new NpgsqlConnection(PGadminConnection))
                {
                    returnPerson = connection.Query<Person>(SQLJoinArea).First();
                    Console.WriteLine(SQLJoinArea);
                    return returnPerson;
                }
                
            }
            catch
            {

                var SQL = $"SELECT * from public.person WHERE emailaddress ilike '{email}'";
                using (var connection = new NpgsqlConnection(PGadminConnection))
                {
                    returnPerson = connection.Query<Person>(SQL).First();
                    Console.WriteLine(SQL);
                    return returnPerson;
                }
                
            }
            
        }

        public Person ReadPersonJoinArea(int personId){
            var SQL = $"SELECT person.personid, iscoordinator, emailaddress, firstname, lastname," +
                $"dateofbirth, address, postalcode, city, country, nationality, danishlevel, gender, membershippaid," +
                $"phonenumber, areaid " +
                $"FROM public.person " +
                $"INNER JOIN public.personassignment on personassignment.personid = person.personid " +
                $"where person.personid = {personId}";
            
            Console.WriteLine(SQL);

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            
            try
            {
                Person returnPerson = new Person();
                using (var connection = new NpgsqlConnection(PGadminConnection))
                {
                    returnPerson = connection.Query<Person>(SQL).First();
                    return returnPerson;
                }
            }
            catch
            {
                Person returnPersonError = new Person();
                returnPersonError.areaId = -1;
                return returnPersonError;
            }
            
        }


        //Læser alle Persons objekter fra databasen
        public List<Person> ReadAllPersons()
        {
            Console.WriteLine("ReadAllPersons");
            var SQL = "SELECT  * FROM public.person";
            List<Person> returnList = new List<Person>();

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnList = connection.Query<Person>(SQL).ToList();

            }

            return returnList;
        }

        public List<Person> ReadAllAssignedPersons()
        {
            Console.WriteLine("ReadAllPersons");
            var SQL = "SELECT  * FROM public.person";
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
        public bool UpdatePerson(Person person){

            Console.WriteLine("UpdatePerson");
            var sql = $"UPDATE public.person SET " +
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
                $"membershippaid ={person.MembershipPaid}, " +
                $"phonenumber = {person.PhoneNumber}" +
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
            var sql = $"DELETE FROM public.person WHERE personid = {personId}";

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

