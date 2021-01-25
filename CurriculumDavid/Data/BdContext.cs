using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CurriculumDavid.Models;

namespace CurriculumDavid.Data
{
    public class BdContext : DbContext
    {
        public BdContext (DbContextOptions<BdContext> options)
            : base(options)
        {
        }

        public DbSet<CurriculumDavid.Models.Informacao> Informacao { get; set; }
    }
}
