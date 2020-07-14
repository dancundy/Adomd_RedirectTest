using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace api
{
    public class AasTokenManager
    {
        /// <summary>
        /// Get access token required for getting a connection to an Azure Analysis Service Model.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="applicationSecret"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync(string applicationId, string applicationSecret, string tenant)
        {
            // Authentication using app credentials
            var credential = new ClientCredential(applicationId, applicationSecret);
            string aasUrl = "https://westeurope.asazure.windows.net/";

            string baseResourceUrl = $"https://login.microsoftonline.com/{tenant}";

            // Authenticate using created credentials
            var authenticationContext = new AuthenticationContext(baseResourceUrl);
            var authenticationResult = await authenticationContext.AcquireTokenAsync(aasUrl, credential);

            return authenticationResult?.AccessToken;
        }
    }
}
