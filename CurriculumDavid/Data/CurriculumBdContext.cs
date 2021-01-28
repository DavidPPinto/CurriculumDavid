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

        public DbSet<CurriculumDavid.Models.ExpProfissional> ExpProfissional { get; set; }

        public DbSet<CurriculumDavid.Models.EduFor> EduFor { get; set; }

        public DbSet<CurriculumDavid.Models.ComLing> ComLing { get; set; }
    }
}
