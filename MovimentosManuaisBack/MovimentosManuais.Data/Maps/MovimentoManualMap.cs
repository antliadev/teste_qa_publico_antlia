using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovimentosManuais.Domain.Entitites;

namespace MovimentosManuais.Data.Maps
{
    public class MovimentoManualMap : IEntityTypeConfiguration<MovimentoManual>
    {
        public void Configure(EntityTypeBuilder<MovimentoManual> builder)
        {
            builder.ToTable("MOVIMENTO_MANUAL");

            builder.HasKey(e => e.COD_MOVIMENTO_MANUAL);

            builder.Property(e => e.COD_MOVIMENTO_MANUAL).HasColumnName("COD_MOVIMENTO_MANUAL");

            builder.Property(e => e.COD_COSIF).HasColumnName("COD_COSIF");

            builder.Property(e => e.COD_PRODUTO).HasColumnName("COD_PRODUTO");

            builder.Property(e => e.COD_USUARIO).HasColumnName("COD_USUARIO").HasMaxLength(15).IsUnicode(false);

            builder.Property(e => e.DAT_ANO).HasColumnName("DAT_ANO").HasColumnType("int");

            builder.Property(e => e.DAT_MES).HasColumnName("DAT_MES").HasColumnType("int");

            builder.Property(e => e.DAT_MOVIMENTO).HasColumnName("DAT_MOVIMENTO").HasColumnType("smalldatetime");

            builder.Property(e => e.DES_DESCRICAO).HasColumnName("DES_DESCRICAO").HasMaxLength(300).IsUnicode(false);

            builder.Property(e => e.NUM_LANCAMENTO).HasColumnName("NUM_LANCAMENTO").HasColumnType("int");

            builder.Property(e => e.VAL_VALOR).HasColumnName("VAL_VALOR").HasColumnType("numeric(18, 2)");

            builder.HasOne(d => d.Produto)
                .WithMany(p => p.MovimentoManual)
                .HasForeignKey(d => d.COD_PRODUTO)
                .HasConstraintName("FK_MOVIMENTO_MANUAL_PRODUTO");

            builder.HasOne(d => d.ProdutoCosif)
                .WithMany(p => p.MovimentoManual)
                .HasForeignKey(d => d.COD_COSIF)
                .HasConstraintName("FK_MOVIMENTO_MANUAL_PRODUTO_COSIF");
        }
    }
}