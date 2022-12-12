using System;
using Festivalcito.Shared.Models;
using Dapper;
using Npgsql;

namespace Festivalcito.Server.Models.ShiftAssignedRepositoryFolder
{
    public class ShiftAssignedRepository : GlobalConnections, IShiftAssignedRepository
    {
        public ShiftAssignedRepository()
        {
        }


        public bool CreateShiftAssigned(ShiftAssigned shiftAssigned)
        {
            //Tager et ShiftAssigned object og indsætter via SQL statement i vores database
            //Formatere time og float for at det passer med postgreSQL
            Console.WriteLine("CreateShiftAssigned - Repository");
            var sql = $"INSERT INTO public.shiftAssignedList(" +
                $"shiftid, assignmentid)" +
                $"VALUES (" +
                $"{shiftAssigned.ShiftId}," +
                $"'{shiftAssigned.AssignmentId}')";
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
        public ShiftAssigned ReadShiftAssigned(int shiftAssignedListId)
        {
            Console.WriteLine("ReadShiftAssigned");
            var SQL = $"SELECT * from public.ShiftAssignedList WHERE ShiftAssignedListid = {shiftAssignedListId}";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            ShiftAssigned returnShiftAssigned = new ShiftAssigned();
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnShiftAssigned = connection.Query<ShiftAssigned>(SQL).First();
                return returnShiftAssigned;
            }
        }
        public List<ShiftAssigned> ReadAllShiftAssigned()
        {
            Console.WriteLine("ReadAllShiftAssigned");
            var SQL = "SELECT  * FROM public.ShiftAssignedList;";
            List<ShiftAssigned> returnList = new List<ShiftAssigned>();

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnList = connection.Query<ShiftAssigned>(SQL).ToList();

            }

            return returnList;
        }

        public bool DeleteShiftAssigned(int shiftAssignedListId)
        {
            Console.WriteLine("DeleteShiftAssigned");
            var sql = $"DELETE FROM public.ShiftAssignedList WHERE ShiftAssignedListid = {shiftAssignedListId}";


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
                    Console.WriteLine("Couldn't delete the ShiftAssigned in the list");
                    return false;
                }
            }
        }



    }
}