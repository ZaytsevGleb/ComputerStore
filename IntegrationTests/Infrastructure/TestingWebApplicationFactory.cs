﻿using System.Data;
using System.Data.Common;
using System.Linq;
using DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests.Infrastructure;

public class TestingWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private DbConnection _dbConnection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(ApplicationDbContext));
            var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlite(CreateInMemoryDatabase());
            });
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (_dbConnection.State != ConnectionState.Closed)
        {
            _dbConnection.Close();
        }

        base.Dispose(disposing);
    }

    // fall on interceptor
    private DbConnection CreateInMemoryDatabase()
    {
        if (_dbConnection == null)
        {
            _dbConnection = new SqliteConnection("DataSource=:memory:");
            _dbConnection.Open();
        }

        return _dbConnection;
    }
}

