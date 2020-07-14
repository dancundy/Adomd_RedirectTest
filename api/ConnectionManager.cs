using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.Extensions.Configuration;

namespace api
{
    public class ConnectionManager
    {
        private readonly IConfiguration _configuration;

        public ConnectionManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get an open connection to an Azure Analysis Services Model.
        /// This connection uses an effective identity to manage the data that is retrieved, this customData should contain the customerId.
        /// </summary>
        /// <returns>An open Connection</returns>
        public async Task<AdomdConnection> GetOpenAasConnectionAsync()
        {
            AdomdConnection aasConnection = null;

            try
            {
                var clientId = _configuration["ADAuth:ClientId"];
                var secret = _configuration["ADAuth:ClientSecret"];
                var token = await new AasTokenManager().GetAccessTokenAsync(
                    clientId,
                    secret,
                    _configuration["ADAuth:TenantId"]);

                var connStringFormat = _configuration["aas-model-connstring"];
                var connString = connStringFormat.Replace("{accessToken}", token);

                aasConnection = new AdomdConnection(connString);

                aasConnection?.Open();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseAasConnection(aasConnection);
                throw;
            }

            return aasConnection;
        }

        /// <summary>
        /// Safely close Aas connection.
        /// </summary>
        /// <param name="conn">Connection you wish to close</param>
        private void CloseAasConnection(AdomdConnection conn)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
