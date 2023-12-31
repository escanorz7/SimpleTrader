﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;
using SimpleTrader.FinancialModelingPrepAPI;
using System;
using System.Net.Http;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
             {
                 string? connectionString = context.Configuration.GetConnectionString("default");
                 services.AddDbContext<SimpleTraderDbContext>();
             });

            return hostBuilder;
        }
    }
}
