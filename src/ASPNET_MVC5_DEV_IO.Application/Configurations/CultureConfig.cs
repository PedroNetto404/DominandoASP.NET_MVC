using System.Globalization;

namespace ASPNET_MVC5_DEV_IO.Application.Configurations;

public class CultureConfig
{
    public static void RegisterCulture()
    {
        var culture = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.CurrentCulture = culture; 
        CultureInfo.CurrentUICulture = culture;
    }
}