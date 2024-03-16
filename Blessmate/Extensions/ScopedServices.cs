using Blessmate.Helpers;
using Blessmate.Services;
using Blessmate.Services.IServices;

namespace Blessmate.Extensions;

public static class ScopedServices
{
    public static void AddScopedServices(this IServiceCollection services ){

        services.AddScoped<IAuthService,AuthService>();
        services.AddScoped<IEmailConfirmation,EmailConfirmation>();
        services.AddScoped<IResetPassword,ResetPassword>();
        services.AddScoped<IEmailSender,EmailSender>();
        services.AddScoped<ITherapistsService,TherapistsService>();
        services.AddScoped<IDbIntializer,DbIntializer>();
    }

}
