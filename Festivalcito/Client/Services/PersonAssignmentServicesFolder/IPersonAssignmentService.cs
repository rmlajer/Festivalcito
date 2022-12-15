using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.PersonAssignmentServicesFolder
{



    public interface IPersonAssignmentService
    {
        Task<int> CreatePersonAssignment(PersonAssignment ReadPersonAssignment);
        Task<PersonAssignment> ReadPersonAssignment(int ReadPersonAssignmentId);
        Task<PersonAssignment[]?> ReadAllPersonAssignments();
        Task<int> DeletePersonAssignment(int ReadPersonAssignmentId);
    }
}



