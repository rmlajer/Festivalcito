using System;
using System.Net.Http.Json;
using Festivalcito.Shared.Classes;

namespace Festivalcito.Client.Services.LoginCredentialService{

	public class LoginCredentialService : ILoginCredentialService{

        private readonly HttpClient httpClient;

        public LoginCredentialService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> CreateLoginCredentials(LoginCredential loginCredential)
        {
            var response = await httpClient.PostAsJsonAsync<LoginCredential>("api/logincredential/", loginCredential);
            var responseStatusCode = response.StatusCode;
            
            return (int)responseStatusCode;
        }
        public async Task<LoginCredential> ReadLoginCredential(string email)
        {
            return (await httpClient.GetFromJsonAsync<LoginCredential>("api/logincredential/" + email))!;
        }
        public async Task<int> UpdateLoginCredential(LoginCredential loginCredential)
        {
            return (await httpClient.GetFromJsonAsync<int>("api/logincredential/" + loginCredential))!;
        }
        public async Task<int> DeleteLoginCredential(int LoginCredentialId)
        {
            return (await httpClient.GetFromJsonAsync<int>("api/logincredential/" + LoginCredentialId))!;
        }
    }
}

