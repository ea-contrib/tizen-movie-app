namespace TMA.IdentityService.Contracts
{
    public class SamlConfiguration
    {
        /// <summary>
        /// EntityId as presented by the idp. Used as key to configuration.
        /// </summary>
        public string IdentityProviderEntityId { get; set; }

        public string EntityId { get; set; }
        public string MetadataUrl { get; set; }
    }
}