using Newtonsoft.Json;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetPrice(string symbol)
        {
            using (HttpClient client = new())
            {
                string uri = "https://financialmodelingprep.com/api/v3/stock/real-time-price/" + symbol;
                string apiKey = "?apikey=f85011934d721df2d3f8001ba4adf999";

                HttpResponseMessage response = await client.GetAsync(uri + apiKey);
                string jsonRespone = await response.Content.ReadAsStringAsync();

                StockPriceResult result = JsonConvert.DeserializeObject<StockPriceResult>(jsonRespone);

                if (result.Price == 0)
                {
                    throw new InvalidSymbolException(symbol);
                }

                return result.Price;
            }
        }
    }
}
