using System;
using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Data.Maps;
using MovimentosManuais.Domain.Entitites;

namespace MovimentosManuais.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<ProdutoCosif> ProdutoCosif { get; set; }
        public virtual DbSet<MovimentoManual> MovimentoManual { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProdutoMap());
            builder.ApplyConfiguration(new ProdutoCosifMap());
            builder.ApplyConfiguration(new MovimentoManualMap());

            base.OnModelCreating(builder);
        }
    }
}
