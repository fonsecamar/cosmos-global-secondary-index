using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using secondary_index.Models;

namespace secondary_index
{
    public class GetAccountBySsnFunction
    {
        private readonly ILogger<GetAccountBySsnFunction> _logger;

        public GetAccountBySsnFunction(ILogger<GetAccountBySsnFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(GetAccountBySsnFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts/ssn/{ssn}")] HttpRequest req,
            [CosmosDBInput(databaseName: "%CosmosDBDatabaseName%", containerName: "%CosmosDBIndexCollection%", Connection = "CosmosDBConnection", Id = "{ssn}", PartitionKey = "{ssn}")] AccountIndex accountIndex,
            [CosmosDBInput(databaseName: "%CosmosDBDatabaseName%", containerName: "%CosmosDBAccountCollection%", Connection = "CosmosDBConnection")] Container accountContainer)
        {
            if (accountIndex == null)
            {
                return new NotFoundResult();
            }

            var accountResult = await accountContainer.ReadItemAsync<Account>(accountIndex.accountId, new PartitionKey(accountIndex.accountId));

            return new OkObjectResult(accountResult.Resource);
        }
    }
}
