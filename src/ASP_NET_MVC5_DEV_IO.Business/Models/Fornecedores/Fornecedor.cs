using ASP_NET_MVC5_DEV_IO.Business.Core.Models;
using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores
{
    public class Fornecedor : Entity  
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }

        //EF Relational
        public ICollection<Produto> Produtos { get; set; }
    }
}
