using CandidateDetailsFunc;
using CandidateDetailsFunc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: WebJobsStartup(typeof(Startup))]
namespace CandidateDetailsFunc
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {

            string connectionString = "Server=DESKTOP-47V9KC8;Database=CandidateDetails;Trusted_Connection=True;MultipleActiveResultSets=true";

            builder.Services.AddDbContext<CandidateDbContext>(
                options => options.UseSqlServer(connectionString));

            builder.Services.BuildServiceProvider();
        }
    }
}
