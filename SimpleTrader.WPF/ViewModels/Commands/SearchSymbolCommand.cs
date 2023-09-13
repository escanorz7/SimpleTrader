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
        private ISearchSymbolViewModel _searchSymbolViewModel;
        private IStockPriceService _stockPriceService;

        public event EventHandler? CanExecuteChanged;

        public SearchSymbolCommand(ISearchSymbolViewModel searchSymbolViewModel, IStockPriceService stockPriceService)
        {
            _searchSymbolViewModel = searchSymbolViewModel;
            _stockPriceService = stockPriceService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                double stockPrice = await _stockPriceService.GetPrice(_searchSymbolViewModel.Symbol);
                _searchSymbolViewModel.SearchResultSymbol = _searchSymbolViewModel.Symbol.ToUpper();
                _searchSymbolViewModel.StockPrice = stockPrice;
            }
            catch (InvalidSymbolException)
            {
                _searchSymbolViewModel.ErrorMessage = "Symbol Doesn't exist.";
            }
            catch (Exception)
            {
                _searchSymbolViewModel.ErrorMessage = "An error has occured.";
            }
        }
    }
}
