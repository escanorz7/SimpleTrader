using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.Authentication;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IStockPriceService, StockPriceService>();
                services.AddSingleton<IDataService<Account>, AccountDataService>();
                services.AddSingleton<ISellStockService, SellStockService>();
                services.AddSingleton<IBuyStockService, BuyStockService>();
                services.AddSingleton<IMajorIndexService, MajorIndexService>();
                services.AddSingleton<IAccountService, AccountDataService>();
                services.AddSingleton<IAuthenticationService, AuthenticationService>();

                services.AddSingleton<IPasswordHasher, PasswordHasher>();
            });

            return hostBuilder;
        }
    }
}
