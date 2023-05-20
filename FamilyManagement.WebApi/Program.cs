using FamilyManagement.Data;
using FamilyManagement.Model;
using FamilyManagement.Service;

namespace Work.RestApi
{
    class Program
    {
        private static IConfiguration _appSettings;
        static void Main(string[] args)
        {
            var baseDirectory = Directory.GetCurrentDirectory();
            _appSettings = ReadAppSettings(args, baseDirectory, "appsettings.json");
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = _appSettings.GetSection("dbConnectionString").Value;
            builder.Services.AddScoped<EFDataContext>(_ => new EFDataContext(connectionString));
            builder.Services.AddScoped<IFamiliesService, FamiliesService>();
            builder.Services.AddScoped<IFamiliesRepository, FamiliesRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        static IConfiguration ReadAppSettings(string[] args, string baseDirectory, string filename)
        {
            return new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile(filename, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }
    }
}
