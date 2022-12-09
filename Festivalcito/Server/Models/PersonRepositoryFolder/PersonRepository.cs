using System;
using Festivalcito.Shared.Models;
using Dapper;
using Npgsql;

namespace Festivalcito.Server.Models.PersonRepositoryFolder
{
	public class PersonRepository : GlobalConnections, IPersonRepository{

		public PersonRepository(){
            
        }

        public bool CreatePerson(Person person)
        {
            //Tager et Shift object og indsætter via SQL statement i vores database
            //Formatere time og float for at det passer med postgreSQL
            Console.WriteLine("CreateShift - Repository");
            var sql = $"INSERT INTO public.area(areaname) VALUES ('{}');";
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
                    Console.WriteLine("Couldn't add the shift to the list: " + e.Message);
                    return false;
                }
            }
        }
        public Person ReadPerson(int personId)
        {

        }
        public List<Person> ReadAllPersons()
        {

        }
        public bool UpdatePerson(Person person)
        {

        }
        public bool DeletePerson(int personId)
        {

        }
    }
}

