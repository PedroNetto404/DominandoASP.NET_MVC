using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos.Entidades;

namespace ASP_NET_MVC5_DEV_IO.Infrastructure.Data.Mappings
{
    internal class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable("Produtos");
        }
    }
}
