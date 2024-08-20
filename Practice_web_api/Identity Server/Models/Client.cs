namespace Practice_web_api.Identity_Server.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        //public ClientTypes ClientType { get; set; } // e.g., Confidential, Public
        public List<ClientGrantType> AllowedGrantTypes { get; set; }
        public List<ClientRedirectUri> RedirectUris { get; set; }
        public List<ClientScope> ClientScopes { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public bool RequireConsent{ get ; set; }
    }

    public class ClientGrantType
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string GrantType { get; set; }
    }

    public class ClientRedirectUri
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string RedirectUri { get; set; }
    }

    public class ClientScope
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string Scope { get; set; }
    }
}
