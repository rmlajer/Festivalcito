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

          
            return new Shift();
        }



        public List<Shift> ReadAllShifts()
        {
            var SQL = "SELECT * from Shift";
            List<Shift> returnList = new List<Shift>();
            using (var connection = new NpgsqlConnection(new PgAdminContext().connstring))
            {
                returnList = connection.Query<Shift>(SQL).ToList();
                
                foreach (var shiftX in returnList)
                {
                    Console.WriteLine($"{shiftX.ShiftName}");
                }
            }

            return new List<Shift>();

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

