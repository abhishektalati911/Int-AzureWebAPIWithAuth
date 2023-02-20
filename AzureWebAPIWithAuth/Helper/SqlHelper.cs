using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Data.SqlClient;

namespace AzureWebAPIWithAuth.Helper
{
    public class SqlHelper
    {
        public static SqlConnection Getconnectionstring()
        {
            //  To Get Connection String 

            string tenantId = "er3675748-1d31-78r1-r576-r1264567d5b8";
            string clientId = "9850485r-r7ba-3z5j-44u7-45t6uu676";
            string clientSecret = "WeT5F~uy6ge8~EyfIEEDgEuhM7zWe-_c7drtsawDa";

            string keyvaultUrl = "https://abhishekTkeyvault1245.vault.azure.net/";
            string secretName = "databaseconnectionstring";

            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            SecretClient secretClient = new SecretClient(new Uri(keyvaultUrl), clientSecretCredential);

            var secret = secretClient.GetSecret(secretName);

            string connectionString = secret.Value.Value;

            return new SqlConnection(connectionString);
        }
    }
}
