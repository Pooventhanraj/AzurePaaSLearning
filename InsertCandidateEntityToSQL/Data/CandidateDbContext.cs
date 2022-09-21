using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertCandidateEntityToSQL.Data
{
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Candidate> Candidate { get; set; }
    }
}
  
