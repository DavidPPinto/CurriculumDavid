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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComLing>()
                .HasOne(p => p.DadosPessoais) // um comling tem um dados pessoais
                .WithMany(c => c.ComLings) // que por sua vez tem vários comling
                .HasForeignKey(p => p.DadosPessoaisId) // chave estrangeira
                .OnDelete(DeleteBehavior.Restrict); // não permitir o cascade delete

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EduFor>()
               .HasOne(p => p.DadosPessoais) // um comling tem um dados pessoais
               .WithMany(c => c.EduFors) // que por sua vez tem vários comling
               .HasForeignKey(p => p.DadosPessoaisId) // chave estrangeira
               .OnDelete(DeleteBehavior.Restrict); // não permitir o cascade delete

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExpProfissional>()
               .HasOne(p => p.DadosPessoais) // um comling tem um dados pessoais
               .WithMany(c => c.ExpProfissionals) // que por sua vez tem vários comling
               .HasForeignKey(p => p.DadosPessoaisId) // chave estrangeira
               .OnDelete(DeleteBehavior.Restrict); // não permitir o cascade delete

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CurriculumDavid.Models.DadosPessoais> DadosPessoais { get; set; }

        public DbSet<CurriculumDavid.Models.ExpProfissional> ExpProfissional { get; set; }

        public DbSet<CurriculumDavid.Models.EduFor> EduFor { get; set; }

        public DbSet<CurriculumDavid.Models.ComLing> ComLing { get; set; }

        public DbSet<CurriculumDavid.Models.Utilizador> Utilizador { get; set; }
    }
}
