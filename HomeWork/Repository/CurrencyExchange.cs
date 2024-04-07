using HomeWork.IRepository;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;

namespace HomeWork.Repository
{
    public class CurrencyExchange : ICurrencyExchange
    {
        private readonly HttpClient _httpClient;
        public CurrencyExchange(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> ExchangeCurrency(DateTime date, decimal amount, string currencyPair)
        {
            // Convert the date to the required format (e.g., yyyy-MM-dd)
            string formattedDate = date.ToString("yyyy-MM-dd");

            // Construct the URL with the provided parameters
            var apiUrl = $"http://localhost:100/api/CurrencyExchange?date={formattedDate}&amount={amount}&currencyPair={currencyPair}";

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
            var apiUrl = "http://localhost:100/api/CurrencyExchange/currencyPairs";

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
