using System;
using Dapper;
using Npgsql;
using Festivalcito.Shared.Models;

namespace Festivalcito.Server.Models.AreaRepositoryFolder{
	public class AreaRepository : GlobalConnections, IAreaRepository{
		public AreaRepository()
		{
		}


        public bool CreateArea(Area area)
        {
            //Tager et Shift object og indsætter via SQL statement i vores database
            //Formatere time og float for at det passer med postgreSQL
            Console.WriteLine("CreateShift - Repository");
            var sql = $"INSERT INTO public.area(areaname) VALUES ('{area.AreaName}');";
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
        public Area ReadArea(int areaId)
        {
            Console.WriteLine("ReadShift");
            var SQL = $"SELECT * from public.area WHERE areaid = {areaId}";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            Area returnShift = new Area();
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnShift = connection.Query<Area>(SQL).First();
                return returnShift;
            }
        }
        public List<Area> ReadAllAreas()
        {
            Console.WriteLine("ReadAllShifts");
            var SQL = "SELECT  * FROM public.area;";
            List<Area> returnList = new List<Area>();

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            using (var connection = new NpgsqlConnection(PGadminConnection))
            {
                returnList = connection.Query<Area>(SQL).ToList();

            }

            return returnList;
        }
        public bool UpdateArea(Area area)
        {
            Console.WriteLine("UpdateShift");
            var sql = $"UPDATE public.area SET areaname='{area.AreaName}' WHERE areaid = {area.AreaID}";

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
                    Console.WriteLine("Couldn't update the shift in the list: " + e.Message);
                    return false;
                }
            }
        }
        public bool DeleteArea(int areaId)
        {
            Console.WriteLine("DeleteArea");
            var sql = $"DELETE FROM public.area WHERE areaid = {areaId}";


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
                    Console.WriteLine("Couldn't delete the area in the list");
                    return false;
                }
            }
        }



    }
}

