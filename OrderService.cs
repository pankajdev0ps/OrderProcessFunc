using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;


namespace OrderProcess
{
    public class OrderService
    {
        [FunctionName("OrderService")]
        public static void Run(
            [ServiceBusTrigger("orders", Connection = "ServiceBusConnection")] string orderItem,
            [Sql(commandText: "dbo.orders", connectionStringSetting: "SqlConnectionString") ] IAsyncCollector<Order> db,
            ILogger log)
        {
            dynamic data = JsonConvert.DeserializeObject(orderItem);
            
            Order order = new()
            {
                custEmail   = data.CustEmail,
                prodID      = data.ProdID,
                prodCost    = data.ProdCost,
                orderdate   = DateTime.Now
            };

            log.LogInformation("Order Data: " + order);
            
            db.AddAsync(order);
            db.FlushAsync();
        }
    }
}
