using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models.AssignedRepositoryFolder
{
    public interface IAssignedRepository
    {
        bool CreateAssigned(Assigned Assigned);
        Assigned ReadAssigned(int AssignedId);
        List<Assigned> ReadAllAssigned();
        bool DeleteAssigned(int AssignedId);
    }
}



 