using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASP_NET_MVC5_DEV_IO.Infrastructure.Data.Mappings
{
    internal class FornecedorConfig : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome).IsRequired().HasMaxLength(200);
            builder.Property(f => f.Documento).IsRequired().HasMaxLength(14);

            builder.HasMany(f => f.Produtos).WithOne(p => p.Fornecedor).HasForeignKey(p => p.FornecedorId);
            builder.HasOne(f => f.Endereco).WithOne(e => e.Fornecedor).HasForeignKey<Endereco>();

            builder.ToTable("Fornecedores"); 
        }
    }
}
