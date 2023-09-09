using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Authentication;
using SimpleTrader.WPF.Models;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }

        public bool IsAuthenticated => CurrentAccount != null;

        public event Action StateChanged;
        public Account CurrentAccount
        {
            get
            {
                return _accountStore.CurrentAccount;
            }
            set
            {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public async Task<bool> Login(string userName, string password)
        {
            bool result = true;

            try
            {
                CurrentAccount = await _authenticationService.Login(userName, password);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public void Logout()
        {
            CurrentAccount = null;
        }

        public async Task<RegistrationResult> Register(string email, string userName, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, userName, password, confirmPassword);
        }
    }
}
