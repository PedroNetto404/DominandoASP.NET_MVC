using ASP_NET_MVC5_DEV_IO.Business.Core.Data;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades;

namespace ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.DataAbstraction
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId); 
    }
}
