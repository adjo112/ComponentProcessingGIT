using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace ComponentProcessing.EntityModel
{
    public class CompContext : DbContext
    {
        public CompContext(DbContextOptions<CompContext> options)
          : base(options)
        {
        }

        public virtual DbSet<ProcReq> ProcessRequest { get; set; }
        public virtual DbSet<ProcRes> ProcessResponse { get; set; }

        public virtual DbSet<CreditDetail> CredDetails { get; set; }

    }
}
