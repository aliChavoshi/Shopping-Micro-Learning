using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    //Catalog
    private const string CatalogApiScope = "catalogapi";
    private const string CatalogApiReadScope = "catalogapi.read";
    private const string CatalogApiWriteScope = "catalogapi.write";
    //End Catalog
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Phone(),
        new IdentityResources.Email()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new(CatalogApiScope, "Catalog Scope"),
        new(CatalogApiReadScope, "Catalog Read Scope"),
        new(CatalogApiWriteScope, "Catalog Write Scope"),
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new("Catalog", "Catalog.API")
        {
            Scopes = { CatalogApiReadScope , CatalogApiWriteScope }
        }
    ];

    public static IEnumerable<Client> Clients =>
    [
        // m2m ClientCredentials 
         new()
        {
             ClientName = "Catalog Api Client",
             ClientId = "CatalogApiClient",
             AllowedScopes = { CatalogApiReadScope, CatalogApiWriteScope },
             AllowedGrantTypes = GrantTypes.ClientCredentials,
             ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
         },
    ];
}