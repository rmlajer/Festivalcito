using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models.PersonAssignmentRepositoryFolder
{
    public interface IPersonAssignmentRepository
    {
        bool CreatePersonAssignment(PersonAssignment Assigned);
        PersonAssignment ReadPersonAssignment(int PersonAssignmentId);
        List<PersonAssignment> ReadAllPersonAssignments();
        bool DeletePersonAssignment(int AssignedId);
    }
}



