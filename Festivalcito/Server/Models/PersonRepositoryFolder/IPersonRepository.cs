using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models.PersonRepositoryFolder
{
    public interface IPersonRepository
    {
        bool CreatePerson(Person person);
        Person ReadPerson(int personId);
        Person ReadPersonEmail(string email);
        Person ReadPersonJoinArea(int personId);
        List<Person> ReadAllPersons();
        bool UpdatePerson(Person person);
        bool DeletePerson(int personId);
        bool sendEmailToPerson(Person person);

    }
}
