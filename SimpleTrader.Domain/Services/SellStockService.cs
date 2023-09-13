using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services
{
    public class SellStockService : ISellStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _dataAccountService;

        public SellStockService(IStockPriceService stockPriceService, IDataService<Account> dataAccountService)
        {
            _stockPriceService = stockPriceService;
            _dataAccountService = dataAccountService;
        }

        public async Task<Account> SellStock(Account seller, string symbol, int shares)
        {
            int accountShares = GetAccountSharesForSymbol(seller, symbol);
            if (accountShares < shares)
            {
                throw new InsufficientSharesException(symbol, accountShares, shares);
            }

            double stockPrice = await _stockPriceService.GetPrice(symbol);

            seller.AssetTransactions.Add(new AssetTransaction
            {
                Account = seller,
                Asset = new Asset()
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.UtcNow,
                IsPurchase = false,
                Shares = shares
            });

            seller.Balance += shares * stockPrice;

            await _dataAccountService.Update(seller.Id, seller);

            return seller;
        }

        private int GetAccountSharesForSymbol(Account seller, string symbol)
        {
            var accountTransactionsForSymbol = seller.AssetTransactions.Where(a => a.Asset.Symbol == symbol);
            return accountTransactionsForSymbol.Sum(a => a.IsPurchase ? a.Shares : -a.Shares);
        }
    }
}
