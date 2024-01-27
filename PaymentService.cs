using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderProcess
{
    public static class PaymentService
    {
        [FunctionName("PaymentService")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log,
            [ServiceBus("orders", Connection = "ServiceBusConnection")] out String message)
        {
            string requestBody =  new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            message = requestBody;
            log.LogInformation($"Data: {data}");
            return new OkObjectResult(true);
        }
    }
}
