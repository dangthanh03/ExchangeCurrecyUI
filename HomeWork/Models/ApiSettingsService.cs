using Microsoft.Extensions.Configuration;
namespace HomeWork.Models
{
    public class ApiSettingsService
    {
        private readonly IConfiguration _configuration;

        public ApiSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApiConfiguration GetApiConfiguration()
        {
            var apiConfig = new ApiConfiguration();
            _configuration.GetSection("ApiSettings").Bind(apiConfig);
            return apiConfig;
        }
    }
}
