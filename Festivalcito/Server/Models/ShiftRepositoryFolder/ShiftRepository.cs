using System;
using Dapper;
using Npgsql;
using Festivalcito.Shared.Models;
namespace Festivalcito.Server.Models.ShiftRepositoryFolder{


	public class ShiftRepository : GlobalConnections, IShiftRepository{

		public ShiftRepository()
		{
		}

        public bool CreateShift(Shift shift){
            Console.WriteLine("CreateShift - Repository");
            var sql = $"INSERT INTO public.shift(" +
                $"starttime, endtime, requiredvolunteers, agemin, hourmultiplier, islocked, name)" +
                $"VALUES (" +
                $"'{shift.StartTime}'," +
                $"'{shift.EndTime}'," +
                $"{shift.RequiredVolunteers}," +
                $"{shift.AgeMin}," +
                $"{shift.HourMultiplier}," +
                $"{shift.IsLocked}," +
                $"{shift.Name})";

            Console.WriteLine("Name: " + shift.Name);

            using (var connection = new NpgsqlConnection(PGadminConnection)){
                try{
                    Console.WriteLine(shift.StartTime.ToString("yyyy-MM-dd HH-mm-ss"));
                    Console.WriteLine(shift.EndTime);
                    connection.Execute(sql);
                    return true;
                }
                catch (Exception e){
                    Console.WriteLine("Couldn't add the shift to the list" + e.Message);
                    return false;
                }
            }
        }


        public Shift ReadShift(int shiftId){
            Console.WriteLine("ReadShift");
            var SQL = $"SELECT * from Shift WHERE ShiftID = {shiftId}";
            Shift returnShift = new Shift();
            using (var connection = new NpgsqlConnection(PGadminConnection)){
                returnShift = connection.Query<Shift>(SQL).First();

                //returnShift = connection.Query<Shift>(SQL).ToList().First();
                return returnShift;
            }
            
        }

        
        public List<Shift> ReadAllShifts() { 
            Console.WriteLine("ReadAllShifts");
            var SQL = "SELECT  * FROM public.shift;";
            List<Shift> returnList = new List<Shift>();
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnList = connection.Query<Shift>(SQL).ToList();
                
            }

            return returnList;

        }

        public bool UpdateShift(Shift shift)
        {
            Console.WriteLine("UpdateShift");
            var sql = $"UPDATE public.shift " +
                $"SET starttime={shift.StartTime}, endtime={shift.EndTime}, requiredvolunteers={shift.RequiredVolunteers}, agemin={shift.AgeMin}, " +
                $"hourmultiplier={shift.HourMultiplier}, islocked={shift.IsLocked}, name={shift.Name}" +
                $"WHERE shiftid = {shift.ShiftID}";

            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                try
                {
                    connection.Execute(sql);
                    return true;
                }
                catch
                {
                    Console.WriteLine("Couldn't update the shift in the list");
                    return false;
                }
            }
        }
        public bool DeleteShift(int ShiftID){
            Console.WriteLine("DeleteShift");
            var sql = $"DELETE FROM public.shift WHERE shiftid = {ShiftID}";

            using (var connection = new NpgsqlConnection(PGadminConnection)){
                try{
                    connection.Execute(sql);
                    return true;
                }
                catch{
                    Console.WriteLine("Couldn't update the shift in the list");
                    return false;
                }
            }
        }
    }
}

