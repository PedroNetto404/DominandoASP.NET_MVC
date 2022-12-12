using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades;

namespace ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Services
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Task Remover(Guid fornecedorId);
        Task AtualizarEndereco(Endereco endereco);
    }
}
