using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Stock.Infrastructure.Context
{
    public class EfContextFactory : IDesignTimeDbContextFactory<EfContext>
    {
        public EfContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<EfContext>();
            string connectionString = configuration.GetConnectionString("principal");
            builder.UseSqlServer(connectionString);

            return new EfContext(builder.Options);
        }
    }
}
