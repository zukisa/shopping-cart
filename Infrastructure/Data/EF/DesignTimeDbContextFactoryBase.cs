using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Infrastructure.Data.EF
{
    /// <summary>
    /// Base Class for generating database
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            return Create();
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create()
        {
            // Tell app to read json file for ConnectionString on the project path
            // When you a json file make sure you set properties [Copy to Output Directory = Copy Always]
            var build = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            //Read connection string from json file
            string connectionString = build.GetConnectionString("Default");
            if (connectionString.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidOperationException("No ConnectionString named 'Default' was found");
            }

            //Tell application to use SQL Server database with connection string from json file
            var optionsBuilder = new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
