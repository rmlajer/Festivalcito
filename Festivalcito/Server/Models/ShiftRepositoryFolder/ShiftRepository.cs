using System;
using Dapper;
using Npgsql;
using Festivalcito.Shared.Classes;
namespace Festivalcito.Server.Models.ShiftRepositoryFolder{

    //Indeholder metoder til CRUD funktionalitet på tablen shift.
	public class ShiftRepository : GlobalConnections, IShiftRepository{

		public ShiftRepository()
		{
		}

        public bool CreateShift(Shift shift){
            //Tager et Shift object og indsætter via SQL statement i vores database
            //Formatere time og float for at det passer med postgreSQL
            Console.WriteLine("CreateShift - Repository");
            var sql = $"INSERT INTO public.shift(" +
                $"starttime, endtime, requiredvolunteers, agemin, hourmultiplier, islocked, shiftname, areaid)" +
                $"VALUES (" +
                $"'{shift.StartTime.ToString("yyyy-MM-dd HH:mm:ss").Replace("\"", "").Replace(".",":")}'," +
                $"'{shift.EndTime.ToString("yyyy-MM-dd HH:mm:ss").Replace("\"", "").Replace(".", ":")}'," +
                $"{shift.RequiredVolunteers}," +
                $"{shift.AgeMin}," +
                $"'{shift.HourMultiplier.ToString().Replace(",",".")}'," +
                $"{shift.IsLocked}," +
                $"'{shift.ShiftName}'," +
                $"{shift.areaId})";
            Console.WriteLine("sql: " + sql);

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection)){
                try{
                    connection.Execute(sql);
                    return true;
                }
                catch (Exception e){
                    Console.WriteLine("Couldn't add the shift to the list: " + e.Message);
                    return false;
                }
            }


        }

        //Læser et shift objekt fra databasen ved brug af shiftID
        public Shift ReadShift(int shiftId){
            Console.WriteLine("ReadShift");
            var SQL = $"SELECT shiftid, starttime,endtime,requiredvolunteers, " +
                $"agemin,hourmultiplier,islocked,shiftname,areaId " +
                $"from Shift INNER JOIN area on area.areaid = Shift.areaid WHERE shiftid = {shiftId}";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            Shift returnShift = new Shift();
            using (var connection = new NpgsqlConnection(PGadminConnection)){
                returnShift = connection.Query<Shift>(SQL).First();
                return returnShift;
            }
            
        }

        //Læser alle shifts objekter fra databasen
        public List<Shift> ReadAllShifts() { 
            Console.WriteLine("ReadAllShifts");
            var SQL = $"SELECT shiftid, starttime,endtime,requiredvolunteers, " +
                $"agemin,hourmultiplier,islocked,shiftname,areaId " +
                $"from Shift";
            List<Shift> returnList = new List<Shift>();
            Console.WriteLine(SQL);
            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnList = connection.Query<Shift>(SQL).ToList();
                
            }

            return returnList;

        }
        //Overskriver et entry i tablen shift med det nye objeckt shift af klassen Shift
        public bool UpdateShift(Shift shift)
        {
            Console.WriteLine("UpdateShift");
            var sql = $"UPDATE public.shift " +
                $"SET starttime='{shift.StartTime.ToString("yyyy-MM-dd HH:mm:ss").Replace("\"", "").Replace(".", ":")}'," +
                $"endtime='{shift.EndTime.ToString("yyyy-MM-dd HH:mm:ss").Replace("\"", "").Replace(".", ":")}'," +
                $"requiredvolunteers={shift.RequiredVolunteers}," +
                $"agemin={shift.AgeMin}," +
                $"hourmultiplier='{shift.HourMultiplier.ToString().Replace(",", ".")}'," +
                $"islocked={shift.IsLocked}," +
                $"shiftname='{shift.ShiftName}'," +
                $"areaId={shift.areaId} " +
                $"WHERE shiftid = {shift.ShiftID}";

            Console.WriteLine("sql: " + sql);


            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection)){
                try
                {
                    connection.Execute(sql);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Couldn't update the shift in the list: " + e.Message);
                    return false;
                }
            }
        }

        //Sletter et entry ved brug af ShiftId
        public bool DeleteShift(int ShiftId){
            Console.WriteLine("DeleteShift");
            var sql = $"DELETE FROM public.shift WHERE shiftid = {ShiftId}";

            Console.WriteLine(sql);
            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection)){
                try{
                    connection.Execute(sql);
                    return true;
                }
                catch{
                    Console.WriteLine("Couldn't delete the shift in the list");
                    return false;
                }
            }
        }
    }
}

