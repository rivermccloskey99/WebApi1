using System.Text;
using System.Text.Json;
using WebApi1.Dtos;

namespace WebApi1.SyncDataServices.Http
{
    public class HttpCommandDataClient: ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendCustomerToCommand(CustomerReadDto customerReadDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(customerReadDto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(_configuration["CommandsService"], httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("-- POST to CommandsService was OK");
            }
            else
            {
                Console.WriteLine("-- POST to CommandsService was not OK");
            }
        }
    }
}
