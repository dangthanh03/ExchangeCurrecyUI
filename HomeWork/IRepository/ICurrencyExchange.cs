namespace HomeWork.IRepository
{
    public interface ICurrencyExchange
    {

        Task<decimal> ExchangeCurrency(DateTime date, decimal amount, string currencyPair);
        Task<string[]> GetCurrencyPairs();
    }
}
