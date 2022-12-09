﻿using System;
using Festivalcito.Shared.Models;

namespace Festivalcito.Server.Models.PersonRepositoryFolder
{
	public interface IPersonRepository
	{
        bool CreatePerson(Person person);
        Person ReadPerson(int personId);
        List<Person> ReadAllPersons();
        bool UpdatePerson(Person person);
        bool DeletePerson(int personId);
    }
}

