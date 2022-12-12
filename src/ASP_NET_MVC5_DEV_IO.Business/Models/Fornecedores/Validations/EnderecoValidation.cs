using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades;

namespace ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            //Validações de endereço
        }
    }
}
