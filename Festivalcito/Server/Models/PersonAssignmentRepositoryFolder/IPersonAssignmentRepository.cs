using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models.PersonAssignmentRepositoryFolder
{
    public interface IPersonAssignmentRepository
    {
        bool CreateAssigned(PersonAssignment Assigned);
        PersonAssignment ReadAssigned(int AssignedId);
        List<PersonAssignment> ReadAllAssigned();
        bool DeleteAssigned(int AssignedId);
    }
}



 