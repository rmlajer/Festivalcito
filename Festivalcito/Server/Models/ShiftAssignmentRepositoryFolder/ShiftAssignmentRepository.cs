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


        public bool CreateShiftAssigned(ShiftAssignment shiftAssigned)
        {
            //Tager et ShiftAssigned object og indsætter via SQL statement i vores database
            //Formatere time og float for at det passer med postgreSQL
            Console.WriteLine("CreateShiftAssigned - Repository");
            var sql = $"INSERT INTO public.ShiftAssignment(" +
                $"shiftid, assignmentid)" +
                $"VALUES (" +
                $"{shiftAssigned.ShiftId}," +
                $"'{shiftAssigned.ShiftAssignmentid}')";
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
        public ShiftAssignment ReadShiftAssigned(int shiftAssignedListId)
        {
            Console.WriteLine("ReadShiftAssigned");
            var SQL = $"SELECT * from public.ShiftAssignment WHERE ShiftAssignmentid = {shiftAssignedListId}";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            ShiftAssignment returnShiftAssigned = new ShiftAssignment();
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnShiftAssigned = connection.Query<ShiftAssignment>(SQL).First();
                return returnShiftAssigned;
            }
        }
        public List<ShiftAssignment> ReadAllShiftAssigned()
        {
            Console.WriteLine("ReadAllShiftAssigned");
            var SQL = "SELECT  * FROM public.ShiftAssignment;";
            List<ShiftAssignment> returnList = new List<ShiftAssignment>();

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnList = connection.Query<ShiftAssignment>(SQL).ToList();

            }

            return returnList;
        }

        public bool DeleteShiftAssigned(int shiftAssignedListId)
        {
            Console.WriteLine("DeleteShiftAssigned");
            var sql = $"DELETE FROM public.ShiftAssignment WHERE ShiftAssignmentid = {shiftAssignedListId}";


            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection)){
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