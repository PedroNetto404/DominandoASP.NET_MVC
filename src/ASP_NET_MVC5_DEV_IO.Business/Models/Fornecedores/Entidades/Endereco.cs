using ASP_NET_MVC5_DEV_IO.Business.Core.Models;

namespace ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades
{
    public class Endereco : Entity
    {
        public Guid FornecedorId { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        //EF RElations
        public Fornecedor Fornecedor { get; set; }
    }
}
