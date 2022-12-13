using System;
using Festivalcito.Shared.Models;

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



 