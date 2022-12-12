using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades;

namespace ASP_NET_MVC5_DEV_IO.Infrastructure.Data.Mappings
{
    internal class EnderecoConfig : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("Enderecos"); 
        }
    }
}
