using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CandidateDetailsFunc.Data;

namespace CandidateDetailsFunc
{
    public static class CandidateDetails
    {
        [FunctionName("CandidateDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [Queue("CandidateDetailsInbound",Connection = "AzureWebJobsStorage")] IAsyncCollector<Details> candidateDetailsQueue)
           
        {

             string storageconn = "DefaultEndpointsProtocol=https;AccountName=azpaaslearning;AccountKey=hFf80QWEQdNGq9sZ472xj2kdJD8i4u+MqS568tWhcxPDhoByHLBvfFw2xZT3BwcL5tTAqMSfr1sn+AStUMCYKw==;EndpointSuffix=core.windows.net";
            log.LogInformation("C# HTTP trigger function processed a request.");
          
            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Details data = JsonConvert.DeserializeObject<Details>(requestBody);
            //name = name ?? data?.name;
           await candidateDetailsQueue.AddAsync(data);


        //    await outputTable.AddAsync(data);
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
