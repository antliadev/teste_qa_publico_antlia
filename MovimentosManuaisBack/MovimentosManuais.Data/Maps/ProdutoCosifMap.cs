using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovimentosManuais.Domain.Entitites;

namespace MovimentosManuais.Data.Maps
{
    public class ProdutoCosifMap : IEntityTypeConfiguration<ProdutoCosif>
    {
        public void Configure(EntityTypeBuilder<ProdutoCosif> builder)
        {
            builder.ToTable("PRODUTO_COSIF");
            
            builder.HasKey(e => e.COD_COSIF);

            builder.Property(e => e.COD_COSIF).HasColumnName("COD_COSIF").ValueGeneratedNever();

            builder.Property(e => e.COD_CLASSIFICACAO).IsRequired().HasMaxLength(15).IsUnicode(false).HasColumnName("COD_CLASSIFICACAO");

            builder.Property(e => e.COD_PRODUTO).IsRequired().HasColumnName("COD_PRODUTO");

            builder.Property(e => e.STA_STATUS).IsRequired().HasColumnName("STA_STATUS").HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Produto)
                .WithMany(p => p.ProdutoCosif)
                .HasForeignKey(d => d.COD_PRODUTO)
                .HasConstraintName("FK_PRODUTO_COSIF_PRODUTO");
        }
    }
}