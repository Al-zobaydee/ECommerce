using ECommerce.Api.Search.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomersService> logger;

        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<CustomersService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomersService");
                var respone = await client.GetAsync($"api/customers/{customerId}");
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsByteArrayAsync();
                    var result = JsonSerializer.Deserialize<dynamic>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return (true, result, null);
                }
                return (false, null, respone.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
