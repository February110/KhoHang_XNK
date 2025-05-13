using Elfie.Serialization;
using System.Globalization;

namespace KhoHang_XNK.ConfigurationService
{
    public static class ConfigurationService
    {
        public static void RegisterGlobalizationAndLocalization(this IServiceCollection services)
        {
            var defaultCulture = new[]
            {
                new CultureInfo("vi-VN"),
                new CultureInfo("en-US")
            }; 

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("vi-VN");
                options.SupportedCultures = defaultCulture;
                options.SupportedUICultures = defaultCulture;
            });
            services.AddMvc()
       .AddDataAnnotationsLocalization(options => {
           options.DataAnnotationLocalizerProvider = (type, factory) =>
               factory.Create(typeof(Resource));
       });
        }
    }
}
