﻿using ASP_NET_MVC5_DEV_IO.Business.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id); 
    }
}
