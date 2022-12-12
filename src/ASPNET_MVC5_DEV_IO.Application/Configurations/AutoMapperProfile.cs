using ASP_NET_MVC5_DEV_IO.Business.Models.Fornecedores.Entidades;
using ASP_NET_MVC5_DEV_IO.Business.Models.Produtos.Entidades;
using ASPNET_MVC5_DEV_IO.Application.ViewModels;
using AutoMapper;

namespace ASPNET_MVC5_DEV_IO.Application.Configurations;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
        CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        CreateMap<Produto, ProdutoViewModel>().ReverseMap(); 
    }
}