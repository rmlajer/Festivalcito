using System;
using Festivalcito.Shared.Models;

namespace Festivalcito.Client.Services.PersonServicesFolder
{
	public interface IPersonService
	{
        Task<int> CreatePerson(Person person);
        Task<Shift> ReadPerson(int personId);
        Task<Shift[]?> ReadAllPersons();
        Task<int> UpdatePerson(Person person);
        Task<int> DeletePerson(int personID);
    }
}

