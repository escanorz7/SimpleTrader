using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels.Commands
{
    public class SellStockCommand : ICommand
    {
        private readonly SellViewModel _sellViewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(SellViewModel sellViewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            _sellViewModel = sellViewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            _sellViewModel.ErrorMessage = string.Empty;
            _sellViewModel.StatusMessage = string.Empty;

            try
            {
                string symbol = _sellViewModel.Symbol.ToUpper();
                int shares = _sellViewModel.SharesToSell;
                Account account = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, shares);

                _accountStore.CurrentAccount = account;

                _sellViewModel.StatusMessage = $"Successfully Sold {shares} shares of {symbol}";
            }
            catch (InsufficientSharesException)
            {
                _sellViewModel.ErrorMessage = "Account has Insufficient Shares.";
            }
            catch (Exception)
            {
                _sellViewModel.ErrorMessage = "Transaction Failed.";
            }
        }
    }
}
