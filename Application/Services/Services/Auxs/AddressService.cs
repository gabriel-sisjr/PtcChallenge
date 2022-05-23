using Domain.AuxModels;
using Domain.Interfaces.Services.Auxs;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Services.Services.Auxs
{
    public class AddressService : IAddressService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AddressService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Address?> GetAddressByCep(string cep)
        {
            cep = Regex.Replace(cep, "[^0-9_.]+", "", RegexOptions.Compiled);
            var brasilCepUrl = _configuration.GetSection("CepAPIUrl").Value.Replace("{cep}", cep);
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(brasilCepUrl);

            if (response.IsSuccessStatusCode)
            {
                using var content = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<Address>(content);
            }
            return null;
        }
    }
}
