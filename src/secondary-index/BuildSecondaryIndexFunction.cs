using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using secondary_index.Models;

namespace secondary_index
{
    public class BuildSecondaryIndexFunction
    {
        private readonly ILogger _logger;

        public BuildSecondaryIndexFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BuildSecondaryIndexFunction>();
        }

        [Function(nameof(BuildSecondaryIndexFunction))]
        [CosmosDBOutput(databaseName: "%CosmosDBDatabaseName%", containerName: "%CosmosDBIndexCollection%", Connection = "CosmosDBConnection", PartitionKey = "id")]
        public IEnumerable<AccountIndex>? Run([CosmosDBTrigger(
            databaseName: "%CosmosDBDatabaseName%",
            containerName: "%CosmosDBAccountCollection%",
            Connection = "CosmosDBConnection",
            LeaseContainerName = "accountLeases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Account> accounts)
        {
            if (accounts != null && accounts.Any())
            {
                return accounts.Select(p => new AccountIndex { id = p.Ssn, accountId = p.id });
            }

            return null;
        }
    }
}