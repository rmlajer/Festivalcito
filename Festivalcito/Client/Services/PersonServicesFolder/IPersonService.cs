using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.PersonServicesFolder
{
	public interface IPersonService
	{
        Task<int> CreatePerson(Person person);
        Task<Person> ReadPerson(int personId);
        Task<Person> ReadPersonEmail(string email);
        Task<Person[]?> ReadAllPersons();
        Task<int> UpdatePerson(Person person);
        Task<int> DeletePerson(int personID);
    }
}

