using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovimentosManuais.Domain.Entitites;

namespace MovimentosManuais.Data.Maps
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTO");
            
            builder.HasKey(e => e.COD_PRODUTO);

            builder.Property(e => e.COD_PRODUTO).HasColumnName("COD_PRODUTO").ValueGeneratedNever();

            builder.Property(e => e.DES_PRODUTO).IsRequired().HasColumnName("DES_PRODUTO").HasMaxLength(50).IsUnicode(false);

            builder.Property(e => e.STA_STATUS).IsRequired().HasColumnName("STA_STATUS").HasDefaultValueSql("((1))");
        }
    }
}