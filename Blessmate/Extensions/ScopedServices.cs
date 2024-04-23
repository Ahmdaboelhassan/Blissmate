using Blessmate.Helpers;
using Blessmate.Services;
using Blessmate.Services.IServices;

namespace Blessmate.Extensions;

public static class ScopedServices
{
    public static void AddScopedServices(this IServiceCollection services ){

        services.AddScoped<IAuthService,AuthService>();
        services.AddScoped<IEmailConfirmation,EmailConfirmation>();
        services.AddScoped<IMessageService,MessageService>();
        services.AddScoped<IResetPassword,ResetPassword>();
        services.AddScoped<IEmailSender,EmailSender>();
        services.AddScoped<ITherapistsService,TherapistsService>();
        services.AddScoped<IPhotoService,PhotoService>();
        services.AddScoped<IDbIntializer,DbIntializer>();
        services.AddScoped<IPatientService,PatientService>();
        services.AddScoped<IPaymentService,PaymentService>();
        services.AddScoped<IDashbourdService,DashbourdService>();
    }

}
