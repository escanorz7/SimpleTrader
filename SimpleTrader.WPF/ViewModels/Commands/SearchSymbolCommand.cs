using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels.Commands
{
    public class SearchSymbolCommand : ICommand
    {
        private BuyViewModel _buyViewModel;
        private IStockPriceService _stockPriceService;

        public event EventHandler? CanExecuteChanged;

        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            _buyViewModel = buyViewModel;
            _stockPriceService = stockPriceService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            _buyViewModel.ErrorMessage = string.Empty;
            _buyViewModel.StatusMessage = string.Empty;

            try
            {
                double stockPrice = await _stockPriceService.GetPrice(_buyViewModel.Symbol);
                _buyViewModel.SearchResultSymbol = _buyViewModel.Symbol.ToUpper();
                _buyViewModel.StockPrice = stockPrice;
            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Symbol Doesn't exist.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "An error has occured.";
            }
        }
    }
}
