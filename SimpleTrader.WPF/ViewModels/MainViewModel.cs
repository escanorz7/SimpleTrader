using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Commands;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; }
        public ISimpleTraderViewModelAbstractFactory SimpleTraderViewModelAbstractFactory { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator,
            ISimpleTraderViewModelAbstractFactory viewModelAbstractFactory)
        {
            Navigator = navigator;
            Authenticator = authenticator;
            SimpleTraderViewModelAbstractFactory = viewModelAbstractFactory;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(viewModelAbstractFactory, navigator);

            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
