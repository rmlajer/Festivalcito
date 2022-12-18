using System;
using Festivalcito.Shared.Classes;


namespace Festivalcito.Client.Services.LoginCredentialService
{
	public interface ILoginCredentialService
	{
        Task<int> CreateLoginCredentials(LoginCredential loginCredential);
        Task<LoginCredential> ReadLoginCredential(string email);
        Task<int> UpdateLoginCredential(LoginCredential loginCredential);
        Task<int> DeleteLoginCredential(int LoginCredentialId);
    }
}

