using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Services.Services.Interfeces;

namespace QuizService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddSingleton(InitializeDb(services));
        services.AddScoped<IQuizService, Services.Services.QuizService>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private IDbConnection InitializeDb(IServiceCollection services)
    {
        var connectionString = "DataSource=sharedDb;mode=memory;cache=shared";
        var connection = new SqliteConnection(connectionString);
        connection.Open();

        var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
        optionsBuilder.UseSqlite(connectionString);
        services.AddDbContext<RepositoryContext>(options => options.UseSqlite(connectionString));
        var serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        using (var db = scope.ServiceProvider.GetService<RepositoryContext>())
        {
            SeedData(db);
        }

        return connection;
    }

    private static void SeedData(RepositoryContext db)
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        // Migrate up
        var assembly = typeof(Startup).GetTypeInfo().Assembly;
        var migrationResourceNames = assembly.GetManifestResourceNames()
            .Where(x => x.EndsWith(".sql"))
            .OrderBy(x => x);
        if (!migrationResourceNames.Any()) throw new System.Exception("No migration files found!");
        foreach (var resourceName in migrationResourceNames)
        {
            var query = GetResourceText(assembly, resourceName);
            db.Database.ExecuteSqlRaw(query);
        }
    }

    private static string GetResourceText(Assembly assembly, string resourceName)
    {
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}