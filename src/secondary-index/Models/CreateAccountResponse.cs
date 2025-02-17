using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace secondary_index.Models
{
    public class CreateAccountResponse
    {
        [CosmosDBOutput(databaseName: "%CosmosDBDatabaseName%", containerName: "%CosmosDBAccountCollection%", Connection = "CosmosDBConnection", PartitionKey = "id")]
        public Account? Account { get; set; }

        public required HttpResponseData HttpResponse { get; set; }
    }
}
