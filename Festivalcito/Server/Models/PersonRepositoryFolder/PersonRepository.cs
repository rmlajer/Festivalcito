using System;
using Festivalcito.Shared.Models;
using Dapper;
using Npgsql;

namespace Festivalcito.Server.Models.PersonRepositoryFolder
{
    public class PersonRepository : GlobalConnections, IPersonRepository {

        public PersonRepository()
        {
        }    
        

        public bool CreatePerson(Person person)
        {
            //Tager et Person object og indsætter via SQL statement i vores database
            //Formatere time og float for at det passer med postgreSQL
            Console.WriteLine("CreatePerson - Repository");
            var sql = "";
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
            var SQL = $"SELECT * from person WHERE personID = {personId}";

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
            var sql = "";

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
            var sql = $"DELETE FROM public.Person WHERE personid = {personId}";

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
                    Console.WriteLine("Couldn't update the Person from the list");
                    return false;
                }

            }
        }
    }
        
}

