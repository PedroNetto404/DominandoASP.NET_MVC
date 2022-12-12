using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ASPNET_MVC5_DEV_IO.Application.Extensions;

public static class RazorExtensions
{
    public static string FormatarDocumento(this RazorPage page, int tipoPessoa, string documento)
    {
        return tipoPessoa == 1
            ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00")
            : Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
    }

    public static bool PermitirExibicao(this RazorPage page, string claimName, string claimValue)
    {
        return CustomAuthorization.ValidarClaimsUsuario(page.Context, claimName, claimValue);
    }
}