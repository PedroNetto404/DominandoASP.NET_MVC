﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_MVC5_DEV_IO.Application.ViewModels;

public class FornecedorViewModel
{
    public FornecedorViewModel()
    {
        Id = Guid.NewGuid();
    }
    [Key]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(maximumLength:14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 12)]
    public string Documento { get; set; }
    
    [DisplayName("Tipo")]
    public int TipoFornecedor { get; set; }
    
    public EnderecoViewModel Endereco { get; set; }
    
    [DisplayName("Ativo?")]
    public bool Ativo { get; set; }

    //EF Relational
    public ICollection<ProdutoViewModel> Produtos { get; set; }
}