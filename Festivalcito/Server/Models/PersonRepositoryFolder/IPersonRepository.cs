using System;
using Festivalcito.Shared.Models;

namespace Festivalcito.Server.Models.PersonRepositoryFolder
{
	public interface IPersonRepository
	{
        void CreatePerson(Person person);
        void ReadPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}

