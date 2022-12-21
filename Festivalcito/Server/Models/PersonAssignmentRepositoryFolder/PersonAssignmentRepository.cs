using System;
using Dapper;
using Festivalcito.Shared.Classes;
using Npgsql;

namespace Festivalcito.Server.Models.PersonAssignmentRepositoryFolder
{
    public class PersonAssignmentRepository : GlobalConnections, IPersonAssignmentRepository
    {
        public PersonAssignmentRepository()
        {
        }



        public bool CreatePersonAssignment(PersonAssignment personAssignment)
        {
            //Tager et PersonAssignment object og indsætter via SQL statement i vores database
            Console.WriteLine("Repository - CreatePersonAssignment");
            var sql = $"INSERT INTO public.personassignment(" +
                $"areaid, personid)" +
                $"VALUES (" +
               $"{personAssignment.AreaId}," +
                $"{personAssignment.personid})";
            Console.WriteLine("sql: " + sql);

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksekverer sql statement mod database
            using (var connection = new NpgsqlConnection(AzureConnection))
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
        public PersonAssignment ReadPersonAssignment(int personId)
        {
            Console.WriteLine("Repository - ReadPersonAssignment");
            Console.WriteLine("ReadPersonAssignmentId: " + personId);
            var SQL = $"SELECT * from public.personassignment WHERE personid = {personId}";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksekverer sql statement mod database
            PersonAssignment returnAssigned = new PersonAssignment();
            try
            {
                using (var connection = new NpgsqlConnection(AzureConnection))
                {
                    returnAssigned = connection.Query<PersonAssignment>(SQL).First();
                    return returnAssigned;
                }
            }
            catch
            {
                return new PersonAssignment();
            }

        }
        public List<PersonAssignment> ReadAllPersonAssignments()
        {
            Console.WriteLine("Repository - ReadAllPersonAssignments");
            var SQL = "SELECT  * FROM public.personassignment;";
            List<PersonAssignment> returnList = new List<PersonAssignment>();

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksekverer sql statement mod database
            using (var connection = new NpgsqlConnection(AzureConnection))
            {
                returnList = connection.Query<PersonAssignment>(SQL).ToList();

            }

            return returnList;
        }

        public bool DeletePersonAssignment(int AssignedListId)
        {
            Console.WriteLine("Repository - DeletePersonAssignment");
            var sql = $"DELETE FROM public.personassignment WHERE personassignmentid = {AssignedListId}";

            Console.WriteLine(sql);
            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksekverer sql statement mod database
            using (var connection = new NpgsqlConnection(AzureConnection))
            {
                try
                {
                    connection.Execute(sql);
                    return true;
                }
                catch
                {
                    Console.WriteLine("Couldn't delete the personassignment in the list");
                    return false;
                }
            }
        }



    }
}