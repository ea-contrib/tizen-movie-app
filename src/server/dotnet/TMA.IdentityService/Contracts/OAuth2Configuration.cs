namespace TMA.IdentityService.Contracts
{
    public class OAuth2Configuration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public OAuth2Configuration()
        {
        }

        /// <summary>
        /// Gets or sets the authorization endpoint URL.
        /// </summary>
        public string AuthorizationEndpointUrl { get; set; }

        /// <summary>
        /// Gets or sets the token endpoint URL.
        /// </summary>
        public string TokenEndpointUrl { get; set; }

        /// <summary>
        /// Gets or sets the user information endpoint.
        /// </summary>
        public string UserInformationEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        public string ClientSecret { get; set; }

    }
}