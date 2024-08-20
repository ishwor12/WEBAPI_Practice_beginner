using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Practice_web_api;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practice_web_api
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public string _connString;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public ApplicationContext()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = configuration.Build();
            _connString = _configuration.GetConnectionString("AppConnection");
        }

        public ApplicationContext(string application)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = configuration.Build();
            _connString = application switch
            {
                "IdentityServer" => _configuration.GetConnectionString("MFDbConnection"),
                "MutualFund" => _configuration.GetConnectionString("AppConnection"),
                _ => null
            };
        }
        public DbSet<Product>products { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(_connString))
                {
                    optionsBuilder.UseSqlServer("Server=192.168.20.24; Database=MutualFund; uid=rahul; pwd=infodev; MultipleActiveResultSets=true; Trusted_Connection=false; Connection Timeout=100;Integrated Security=false; PersistSecurityInfo=true;TrustServerCertificate=true;");
                }
                else
                {
                    optionsBuilder.UseSqlServer(_connString);
                }
            }
        }

      


    }
}
