using HomeWork.IRepository;
using HomeWork.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;

namespace HomeWork.Repository
{
    public class CurrencyExchange : ICurrencyExchange
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettingsService _apiSettingsService;
        public CurrencyExchange(HttpClient httpClient, ApiSettingsService apiSettingsService)
        {
            _httpClient = httpClient;
            _apiSettingsService = apiSettingsService;
        }
        public async Task<decimal> ExchangeCurrency(DateTime date, decimal amount, string currencyPair)
        {
            var apiConfig = _apiSettingsService.GetApiConfiguration();
            string formattedDate = date.ToString("yyyy-MM-dd");
            var apiUrl = $"{apiConfig.BaseUrl}{apiConfig.CurrencyExchangeEndpoint}?date={formattedDate}&amount={amount}&currencyPair={currencyPair}";

            // Send a GET request to the API
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            // Read the response content as JSON
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response to a dynamic object
            dynamic responseObject = JsonConvert.DeserializeObject(responseBody);

            // Extract and return the exchanged amount from the response
            decimal exchangedAmount = responseObject.exchangedAmount;
            return exchangedAmount;
        }



        public async Task<string[]> GetCurrencyPairs()
        {
            var apiConfig = _apiSettingsService.GetApiConfiguration();
            var apiUrl = $"{apiConfig.BaseUrl}{apiConfig.CurrencyPairsEndpoint}";
            // Gửi yêu cầu HTTP GET đến API và nhận phản hồi
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            // Đọc dữ liệu JSON từ phản hồi
            string responseBody = await response.Content.ReadAsStringAsync();

            // Phân tích dữ liệu JSON và trả về mảng chuỗi
            string[] currencyPairs = System.Text.Json.JsonSerializer.Deserialize<string[]>(responseBody);

            return currencyPairs;
        }

    }
}
