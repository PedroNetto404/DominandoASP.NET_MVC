using ASP_NET_MVC5_DEV_IO.Business.Core.Models;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades;

namespace ASP_NET_MVC5_DEV_IO.Business.Models.Produtos.Entidades
{
    public class Produto : Entity   
    {
        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime Cadastro { get; set; }
        public bool Ativo { get; set; }

        //EF Relations
        public Fornecedor Fornecedor { get; set; }
    }
}
