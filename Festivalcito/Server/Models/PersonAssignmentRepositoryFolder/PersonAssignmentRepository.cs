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
    //Tager et assigned Assigned object og indsætter via SQL statement i vores database
    Console.WriteLine("CreateAssigned - Repository");
    var sql = $"INSERT INTO public.personassignment(" +
        $"AssignedPerson, AreaId)" +
        $"VALUES (" +
       $" {personAssignment.personid}," +
        $"'{personAssignment.AreaId}')";
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
public PersonAssignment ReadPersonAssignment(int personId)
{
            Console.WriteLine("ReadPersonAssignment");
            Console.WriteLine("ReadPersonAssignmentId: " + personId);
    var SQL = $"SELECT * from public.personassignment WHERE personid = {personId}";

            //Isolere "var connection" fra resten af scope ved brug af using
            //forsøger at eksikvere sql statement mod database
            PersonAssignment returnAssigned = new PersonAssignment();
    using (var connection = new NpgsqlConnection(PGadminConnection))
    {
        returnAssigned = connection.Query<PersonAssignment>(SQL).First();
        return returnAssigned;
    }
}
public List<PersonAssignment> ReadAllPersonAssignments()
{
    Console.WriteLine("ReadAllAssigned");
    var SQL = "SELECT  * FROM public.personassignment;";
    List<PersonAssignment> returnList = new List<PersonAssignment>();

    //Isolere "var connection" fra resten af scope ved brug af using
    //forsøger at eksikvere sql statement mod database
    using (var connection = new NpgsqlConnection(PGadminConnection))
    {
        returnList = connection.Query<PersonAssignment>(SQL).ToList();

    }

    return returnList;
}

public bool DeletePersonAssignment(int AssignedListId)
{
    Console.WriteLine("DeleteAssigned");
    var sql = $"DELETE FROM public.personassignment WHERE personassignmentid = {AssignedListId}";


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