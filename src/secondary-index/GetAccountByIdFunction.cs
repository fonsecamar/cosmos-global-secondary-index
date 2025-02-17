using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using secondary_index.Models;

namespace secondary_index
{
    public class GetAccountByIdFunction
    {
        private readonly ILogger<GetAccountByIdFunction> _logger;

        public GetAccountByIdFunction(ILogger<GetAccountByIdFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(GetAccountByIdFunction))]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "accounts/{id}")] HttpRequest req,
            [CosmosDBInput(databaseName: "%CosmosDBDatabaseName%", containerName: "%CosmosDBAccountCollection%", Connection = "CosmosDBConnection", Id = "{id}", PartitionKey = "{id}")] Account account,
            ILogger log)
        {
            if (account == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(account);
        }
    }
}