using System;
using Festivalcito.Shared.Classes;
using Dapper;
using Npgsql;

namespace Festivalcito.Server.Models.ShiftAssignmentRepositoryFolder
{
    public class ShiftAssignmentRepository : GlobalConnections, IShiftAssignmentRepository
    {
        public ShiftAssignmentRepository()
        {
        }


        public bool CreateShiftAssignment(ShiftAssignment shiftAssigned)
        {
            //Tager et ShiftAssignment object og indsætter via SQL statement i vores database
            //Formatere time og float for at det passer med postgreSQL
            Console.WriteLine("Repository - CreateShiftAssignment");
            Console.WriteLine($"" +
                $"ShiftAssignmentid:{shiftAssigned.ShiftAssignmentid}," +
                $"personassignmentid:{shiftAssigned.personassignmentid} " +
                $"ShiftId:{shiftAssigned.ShiftId}");
            var sql = $"INSERT INTO public.ShiftAssignment(" +
                $"shiftid, personassignmentid)" +
                $"VALUES (" +
                $"{shiftAssigned.ShiftId}," +
                $"{shiftAssigned.personassignmentid})";
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
                    Console.WriteLine("Couldn't add the shift to the list: " + e.Message);
                    return false;
                }
            }
        }
        public ShiftAssignment ReadShiftAssignment(int ShiftAssignmentId)
        {
            Console.WriteLine("Repository - ReadShiftAssignment");
            var SQL = $"SELECT * from public.ShiftAssignment WHERE ShiftAssignmentid = {ShiftAssignmentId}";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksekverer sql statement mod database
            ShiftAssignment returnShiftAssigned = new ShiftAssignment();
            using (var connection = new NpgsqlConnection(AzureConnection))
            {
                returnShiftAssigned = connection.Query<ShiftAssignment>(SQL).First();
                return returnShiftAssigned;
            }
        }
        public List<ShiftAssignment> ReadAllShiftAssignments()
        {
            Console.WriteLine("Repository - ReadAllShiftAssignments");
            var SQL = "SELECT  * FROM public.ShiftAssignment;";
            List<ShiftAssignment> returnList = new List<ShiftAssignment>();

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksekverer sql statement mod database
            using (var connection = new NpgsqlConnection(AzureConnection))
            {
                returnList = connection.Query<ShiftAssignment>(SQL).ToList();

            }

            return returnList;
        }

        public bool DeleteShiftAssignment(int shiftAssignedListId)
        {
            Console.WriteLine("Repository - DeleteShiftAssignment");
            var sql = $"DELETE FROM public.ShiftAssignment WHERE ShiftAssignmentid = {shiftAssignedListId}";


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
                    Console.WriteLine("Couldn't delete the ShiftAssigned in the list");
                    return false;
                }
            }
        }



    }
}