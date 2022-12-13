using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.AssignedServicesFolder
{



    public interface IAssignedService
    {
        Task<int> CreateAssigned(Assigned Assigned);
        Task<Assigned> ReadAssigned(int AssignedListId);
        Task<Assigned[]?> ReadAllAssigned();
        Task<int> DeleteAssigned(int AssignedListId);
    }
}



