using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MaternityHospitalBrui.States;
using MaternityHospitalBrui.Features;

namespace MaternityHospitalBrui.DB
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPatientFeature, PatientFeature>();

            return services;
        }
    }
}