using HomeWork.IRepository;
using HomeWork.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers
{
    public class CurrencyExchangeController : Controller
    {
        private readonly ICurrencyExchange _currencyExchange;

        public CurrencyExchangeController(ICurrencyExchange currencyExchange)
        {
            _currencyExchange = currencyExchange;
        }
        public async Task<IActionResult> Index()
        {
            string[] currencyPairs = await _currencyExchange.GetCurrencyPairs();
            return View(currencyPairs);
        }
        [HttpPost]
        public async Task<IActionResult> ExchangeCurrency(DateTime date, decimal amount, string currencyPair)
        {
           

            decimal exchangedAmount = await _currencyExchange.ExchangeCurrency(date, amount, currencyPair); // Tham số thứ hai không được sử dụng trong phương thức này, bạn có thể cải tiến sau

            // Pass data to the view
            ViewBag.OriginalAmount = amount;
            ViewBag.ExchangedAmount = exchangedAmount;
            ViewBag.CurrencyPair = currencyPair;

            // Return the same view
            return View("Index", _currencyExchange.GetCurrencyPairs().Result);
        }
    }
}
