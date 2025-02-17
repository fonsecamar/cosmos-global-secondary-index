using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using secondary_index.Models;

namespace secondary_index
{
    public class CreateAccountFunction
    {
        private readonly ILogger<CreateAccountFunction> _logger;

        public CreateAccountFunction(ILogger<CreateAccountFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(CreateAccountFunction))]
        public async Task<CreateAccountResponse> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var account = JsonConvert.DeserializeObject<Account>(requestBody);

            var response = req.CreateResponse();
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            if (account == null || string.IsNullOrEmpty(account.Ssn))
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.WriteString("Invalid account data.");
                return new CreateAccountResponse()
                {
                    HttpResponse = response
                };
            }

            account.id = Guid.NewGuid().ToString();
            account.CreatedAt = DateTime.UtcNow;

            response.StatusCode = System.Net.HttpStatusCode.OK;
            return new CreateAccountResponse()
            {
                Account = account,
                HttpResponse = req.CreateResponse(System.Net.HttpStatusCode.OK)
            };
        }
    }
}
