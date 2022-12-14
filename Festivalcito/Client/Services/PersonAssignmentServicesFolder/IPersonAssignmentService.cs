using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.PersonAssignmentServicesFolder
{



    public interface IPersonAssignmentService
    {
        Task<int> CreateAssigned(PersonAssignment Assigned);
        Task<PersonAssignment> ReadAssigned(int AssignedListId);
        Task<PersonAssignment[]?> ReadAllAssigned();
        Task<int> DeleteAssigned(int AssignedListId);
    }
}



