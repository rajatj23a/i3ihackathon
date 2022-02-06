using DecodingFunction.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DecodingFunction
{
    public static class DecodingFunction
    {
        [FunctionName("DecodingFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            log.LogInformation("Passed Type of the request:" + req.Query["type"].ToString());

            // http://localhost:7071/api/DecodingFunction?type=realtimepremium&resultTable=tblResultPremiun&uid=397-78-800-0212&area=Delhi
            // http://localhost:7071/api/DecodingFunction?type=focusgroup&resultTable=tblResultFocusGroup&area=Delhi

            var result = Executor.Execute(req);

            log.LogInformation("C# HTTP trigger function processed request completed.");

            return new OkObjectResult(result);
        }
    }
}
