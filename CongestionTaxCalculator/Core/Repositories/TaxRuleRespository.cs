using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CongestionTaxCalculator.Core.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Repositories
{
    public class TaxRuleRespository : ITaxRuleRespository
    {
        private readonly string _city;
        private readonly string _year;
        private readonly string _databaseName;
        private readonly string _taxContainerName;
        private readonly string _freeDateContainerName;

        private CosmosClient _cosmosClient;
        private Database _cosmosDatabase;
        private Container _cosmosTaxContainer;
        private Container _cosmosFreeDateContainer;

        public TaxRuleRespository(IConfiguration configuration)
        {
            _city = configuration.GetSection("Azure").GetValue<string>("City");
            _year = configuration.GetSection("Azure").GetValue<string>("Year");
            _databaseName = configuration.GetSection("Azure").GetValue<string>("DatabaseName");
            _taxContainerName = configuration.GetSection("Azure").GetValue<string>("TaxContainerName");
            _freeDateContainerName = configuration.GetSection("Azure").GetValue<string>("FreeContainerName");

            this._cosmosClient = new CosmosClient(GetDBConnectionString(), new CosmosClientOptions() { ApplicationName = "TaxCalculator" });
            this._cosmosDatabase = _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName).Result;
            this._cosmosTaxContainer = _cosmosDatabase.CreateContainerIfNotExistsAsync(_taxContainerName, "/city").Result;
            this._cosmosFreeDateContainer = _cosmosDatabase.CreateContainerIfNotExistsAsync(_freeDateContainerName, "/year").Result;
        }

        public async Task<IEnumerable<FreeDate>> GetCongestionFreeDate()
        {
            var sqlQueryText = string.Format("SELECT * FROM c WHERE c.year = '{0}'", _year);

            return await GetAll<FreeDate>(sqlQueryText, this._cosmosFreeDateContainer);
        }

        public async Task<IEnumerable<TaxRule>> GetCongestionTaxTime()
        {
            var sqlQueryText = string.Format("SELECT * FROM c WHERE c.city = '{0}'", _city);

            return await GetAll<TaxRule>(sqlQueryText, this._cosmosTaxContainer);
        }


        private async Task<IEnumerable<T>> GetAll<T>(string query, Container c)
        {
            List<T> objList = new List<T>();

            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<T> queryResultSetIterator = c.GetItemQueryIterator<T>(queryDefinition);

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<T> currentResult = await queryResultSetIterator.ReadNextAsync();
                foreach (T rule in currentResult)
                {
                    objList.Add(rule);
                }

            }
            return objList;
        }

        private string GetDBConnectionString()
        {
            string secretName = "CosmosDBConnectionString";
            var kvUri = Environment.GetEnvironmentVariable("VaultUri");
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
            };

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(), options);
            KeyVaultSecret secret = client.GetSecret(secretName);

            return secret.Value;
        }
    }
}
