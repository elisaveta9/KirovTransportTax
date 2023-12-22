using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Infrastucture;
using KirovTransportTax.Infrastucture.Repositories;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;

namespace KirovTransportTax.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddLinqToDBContext<TransportDbConnection>((provider, options) =>
                options
                .UsePostgreSQL(Configuration.GetConnectionString("PostgreSQL"))
                .UseDefaultLogging(provider));

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IDriverTaxRepository, DriverTaxRepository>();
            services.AddScoped<ITransportModelRepository, TransportModelRepository>();
            services.AddScoped<ITransportRepository, TransportRepository>();
            services.AddScoped<ITransportTaxRepository, TransportTaxRepository>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
                options.JsonSerializerOptions.IncludeFields = true;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
