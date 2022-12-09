using ASP_NET_MVC5_DEV_IO.Business.Core.Services;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Services
{
    internal class FornecedorService : BaseService,IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
            _fornecedorRepository = fornecedorRepository; 
        }
        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (!(ExecutarValidacao(fornecedor, new FornecedorValidation())
                && ExecutarValidacao(fornecedor.Endereco, new EnderecoValidation()))) 
                return;

            if (await ExisteFornecedor(fornecedor)) return; 

            await _fornecedorRepository.Adicionar(fornecedor); 
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(fornecedor, new FornecedorValidation())) return;

            if (await ExisteFornecedor(fornecedor)) return;

            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(endereco, new EnderecoValidation())) return; 
            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid fornecedorId)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(fornecedorId);

            if (fornecedor.Produtos.Any()) return; 

            if(fornecedor.Endereco != null)
            {
                await _enderecoRepository.Remover(fornecedor.Endereco.Id); 
            }

            await _fornecedorRepository.Remover(fornecedorId); 
        }
        public void Dispose()
        {
            _enderecoRepository?.Dispose(); //? --> Caso a instância não exista, o método não é chamado.
            _enderecoRepository?.Dispose(); 
        }
        private async Task<bool> ExisteFornecedor(Fornecedor fornecedor) 
        {
            var forn = await _fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && fornecedor.Id != fornecedor.Id);
            return forn.Any(); 
        }
    }
}

      