using System;
using InsertCandidateEntityToSQL.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace InsertCandidateEntityToSQL
{
    public class CandidateToSQL
    {
        private readonly CandidateDbContext _db;

        public CandidateToSQL(CandidateDbContext db)
        {
            _db = db;
        }

        [FunctionName("CandidateToSQL")]
        public void Run([QueueTrigger("candidatedetailsinbound", Connection = "AzureWebJobsStorage")] Candidate candidateQueueItem, ILogger log)
        {

            _db.Add(candidateQueueItem);
            _db.SaveChanges();
            log.LogInformation($"C# Queue trigger function processed: {candidateQueueItem}");
        }
    }
}
