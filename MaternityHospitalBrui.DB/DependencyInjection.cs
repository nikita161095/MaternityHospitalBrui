using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MaternityHospitalBrui.DB.Context;
using MaternityHospitalBrui.Enums;
using MaternityHospitalBrui.Repository;
using System.Reflection;

namespace MaternityHospitalBrui.DB {
  public static class DependencyInjection {
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment){
      services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(Gender)));

      DbContextOptionsBuilder options = new DbContextOptionsBuilder();
      if (environment.IsDevelopment()) {
        services.AddDbContext<MaternityHospitalBruiContext>(x => TestDb(x, configuration));
      } else {
        services.AddDbContext<MaternityHospitalBruiContext>(x => ProductionDb(x, configuration));
      }

      services.AddTransient<IAppDbContext, AppDbContext>();

      return services;
    }

    private static void TestDb(DbContextOptionsBuilder o, IConfiguration configuration) {
      o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(MaternityHospitalBruiContext).Assembly.FullName));
      //o.UseInMemoryDatabase(databaseName: "Test")
      //  .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }

    private static void ProductionDb(DbContextOptionsBuilder o, IConfiguration configuration) {
      o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly(typeof(MaternityHospitalBruiContext).Assembly.FullName));
    }
  }
}
