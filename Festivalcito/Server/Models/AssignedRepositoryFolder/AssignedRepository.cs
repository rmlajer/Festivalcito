using System;
using Dapper;
using Festivalcito.Shared.Classes;
using Npgsql;

namespace Festivalcito.Server.Models.AssignedRepositoryFolder
{
    public class AssignedRepository : GlobalConnections, IAssignedRepository
    { 
        public AssignedRepository()
        {
        }



public bool CreateAssigned(Assigned Assigned)
{
    //Tager et assigned Assigned object og indsætter via SQL statement i vores database
    Console.WriteLine("CreateAssigned - Repository");
    var sql = $"INSERT INTO public.assignedList(" +
        $"AssignedPerson, AreaId)" +
        $"VALUES (" +
       $" { Assigned.AssignedPerson}," +
        $"'{Assigned.AreaId}')";
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
            Console.WriteLine("Couldn't add the assigned to the list: " + e.Message);
            return false;
        }
    }
}
public Assigned ReadAssigned(int AssignmentId)
{
    Console.WriteLine("ReadAssigned");
    var SQL = $"SELECT * from public.assignedList WHERE AssignedListid = {AssignmentId}";

    //Isolere "var connection" fra resten af scope ved brug af using
    //forsøger at eksikvere sql statement mod database
    Assigned returnAssigned = new Assigned();
    using (var connection = new NpgsqlConnection(PGadminConnection))
    {
        returnAssigned = connection.Query<Assigned>(SQL).First();
        return returnAssigned;
    }
}
public List<Assigned> ReadAllAssigned()
{
    Console.WriteLine("ReadAllAssigned");
    var SQL = "SELECT  * FROM public.assignedList;";
    List<Assigned> returnList = new List<Assigned>();

    //Isolere "var connection" fra resten af scope ved brug af using
    //forsøger at eksikvere sql statement mod database
    using (var connection = new NpgsqlConnection(PGadminConnection))
    {
        returnList = connection.Query<Assigned>(SQL).ToList();

    }

    return returnList;
}

public bool DeleteAssigned(int AssignedListId)
{
    Console.WriteLine("DeleteAssigned");
    var sql = $"DELETE FROM public.assignedList WHERE AssignedListid = {AssignedListId}";


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
            Console.WriteLine("Couldn't delete the Assigned in the list");
            return false;
        }
    }
}



    }
}