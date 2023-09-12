using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels.Commands
{
    public class BuyStockCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;
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
                string symbol = _buyViewModel.Symbol.ToUpper();
                int shares = _buyViewModel.SharesToBuy;
                Account account = await _buyStockService.BuyStock(_accountStore.CurrentAccount, symbol, shares);

                _accountStore.CurrentAccount = account;

                _buyViewModel.StatusMessage = $"Successfully purchased {shares} shares of {symbol}";
            }
            catch (InsufficientFundsException)
            {
                _buyViewModel.ErrorMessage = "Account has Insufficient Funds.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "Transaction Failed.";
            }
        }
    }
}
