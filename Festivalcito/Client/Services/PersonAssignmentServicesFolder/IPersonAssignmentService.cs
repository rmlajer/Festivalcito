using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.PersonAssignmentServicesFolder
{



    public interface IPersonAssignmentService
    {
        Task<int> CreatePersonAssignment(PersonAssignment Assigned);
        Task<PersonAssignment> ReadPersonAssignment(int AssignedListId);
        Task<PersonAssignment[]?> ReadAllPersonAssignments();
        Task<int> DeletePersonAssignment(int AssignedListId);
    }
}



