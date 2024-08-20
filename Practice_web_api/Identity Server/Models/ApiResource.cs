using IdentityServer4.Models;
using Microsoft.AspNetCore.DataProtection;

namespace Practice_web_api.Identity_Server.Models
{
    public class ApiResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Enabled { get; set; }
        public ICollection<string> Scopes { get; set; }
        public ICollection<ApiScope> Scopes { get; set; } // For more complex scenarios
        public ICollection<ApiSecret> ApiSecrets { get; set; }
        public ICollection<string> UserClaims { get; set; }
    }
    public class ApiScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument
        {
            get; set;
        }
    }

    public class ApiSecret
    {
        public int Id { get; set; }
        public ApiResource ApiResource { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
}
