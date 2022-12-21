using System;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Server.Models.LoginCredentialRepositoryFolder
{

    public interface ILoginCredentialRepository
    {

        bool CreateLoginCredential(LoginCredential loginCredential);
        LoginCredential ReadLoginCredential(string email);
        List<LoginCredential> ReadAllLoginCredentials();
        bool DeleteLoginCredential(string email);

    }
}

