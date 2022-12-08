using System;
using Dapper;
using Npgsql;
using Festivalcito.Shared.Models;
namespace Festivalcito.Server.Models.ShiftRepositoryFolder{


	public class ShiftRepository : IShiftRepository{


		public ShiftRepository()
		{
		}

        public bool CreateShift(Shift shift)
        {
            return false;
        }


        public Shift ReadShift(int shiftId){

            var SQL = $"SELECT * from Shift WHERE ShiftID = {shiftId}";
            Shift returnShift = new Shift();
            using (var connection = new NpgsqlConnection(new PgAdminContext().connstring))
            {
                returnShift = connection.Query<Shift>(SQL).First();

                //returnShift = connection.Query<Shift>(SQL).ToList().First();

                Console.WriteLine($"{returnShift.Name}");
                return returnShift;
            }
            
        }

        
        public List<Shift> ReadAllShifts()
        {
            Console.WriteLine("ReadAllShifts");
            var SQL = "SELECT  * FROM public.shift;";
            List<Shift> returnList = new List<Shift>();
            using (var connection = new NpgsqlConnection(new PgAdminContext().connstring))
            {
                returnList = connection.Query<Shift>(SQL).ToList();
                
                foreach (var shiftX in returnList)
                {
                    Console.WriteLine($"{shiftX.Name}");
                }

                Console.WriteLine($"Repository : {returnList.Count()}");
            }

            return returnList;

        }

        public bool UpdateShift(Shift shift)
        {
            return false;
        }
        public bool DeleteShift(int ShiftID)
        {

            return false;
        }
    }
}

