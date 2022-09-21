using System;
using CandidateDetailsFunc.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CandidateDetailsFunc
{
    public class UpdateCandidateDB
    {
        private readonly CandidateDbContext _db;

        public UpdateCandidateDB(CandidateDbContext db)
        {
            _db = db;
        }
        [FunctionName("UpdateCandidateDB")]
        public void Run([QueueTrigger("candidatedetailsinbound", Connection = "AzureWebJobsStorage")] Candidate candidateDetailsQueueItem, ILogger log)
        {
            _db.Add(candidateDetailsQueueItem);
            _db.SaveChanges();
            log.LogInformation($"C# Queue trigger function processed: {candidateDetailsQueueItem}");
        }
    }
}
