using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using Practice_web_api.IdentityModel;

namespace Domain
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {

        private readonly IConfiguration _configuration;

        public string _connString;

        public UserDbContext()
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = configuration.Build();
            _connString = _configuration.GetConnectionString("AppConnection");
        }

        public UserDbContext(string application)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = configuration.Build();
            _connString = application switch
            {
                "IdentityServer" => _configuration.GetConnectionString("MFDbConnection"),
                "MutualFund" => _configuration.GetConnectionString("AppConnection"),
                "CloseMutualFund" => _configuration.GetConnectionString("AppConnection"),
                _ => null
            };
        }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(_connString))
                {
                    optionsBuilder.UseSqlServer("Server=192.168.20.24; Database=UserTable; uid=rahul; pwd=infodev; MultipleActiveResultSets=true; Trusted_Connection=false; Connection Timeout=100;Integrated Security=false; PersistSecurityInfo=true;TrustServerCertificate=true;");
                }
                else
                {
                    optionsBuilder.UseSqlServer(_connString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.HasDefaultSchema("idn");

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))

            {

                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            }

            builder.Entity<ApplicationUser>().HasIndex(a => a.UserName).IsUnique();


        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

      

       
       

    }
}