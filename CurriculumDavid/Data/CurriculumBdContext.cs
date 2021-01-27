using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CurriculumDavid.Models;

namespace CurriculumDavid.Data
{
    public class CurriculumBdContext : DbContext
    {
        public CurriculumBdContext (DbContextOptions<CurriculumBdContext> options)
            : base(options)
        {
        }

        public DbSet<CurriculumDavid.Models.DadosPessoais> DadosPessoais { get; set; }
    }
}
