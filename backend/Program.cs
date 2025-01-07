
using System;
using System.Linq;

using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Npgsql;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner.Conventions;
using backend.Interfaces;
using backend.Services;
namespace backend;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            //creates a builder for setting up a web application (likely using ASP.NET Core).
            var builder = WebApplication.CreateBuilder(args);

            // Dependency Injection Setup
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ICustomerService ,CustomerService>();
            builder.Services.AddTransient<ISalesService,SalesService>();

            // FluentMigrator Setup
            builder.Services.AddFluentMigratorCore()
                .AddSingleton<IConventionSet>(new DefaultConventionSet("jiji", null))
                .ConfigureRunner(rb => rb
                    // Add postgresql support to FluentMigrator
                    .AddPostgres11_0()
                    // Set the connection string
                    // SEARCH_PATH: ;
                    .WithGlobalConnectionString("Host=192.168.1.10;Port=5432;Database=test;User Id=postgres;Password=PostgresDB@dm1n;SearchPath=jiji;")
                    .WithRunnerConventions(new MigrationRunnerConventions())
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(AddLogTable1).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            // Controller and Swagger Setup
            builder.Services.AddControllers();//allowing the application to respond to HTTP requests.
            builder.Services.AddSwaggerGen();//Registers Swagger to generate API documentation.
            

            //Cross-Origin Resource Sharing (CORS) Configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                                builder =>
                                {
                                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                                });
            });
            
            //Building and Running the Application
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();// Enables Swagger to expose the API documentation.
                app.UseSwaggerUI();//Configures Swagger UI to view and test the API.
            }
            app.UseCors("AllowAll");//Enables the CORS policy.
            app.UseHttpsRedirection();//Redirects HTTP requests to HTTPS.
            app.MapControllers();//Maps routes to the controllers.

            //Database Migration
            using var scope = app.Services.CreateScope();//creates a scope for dependency injection (DI). Scoped services are created and disposed of within the scope
            UpdateDatabase(scope.ServiceProvider);//Calls the UpdateDatabase method to run migrations.

            app.Run();
        }
        catch (Exception er)
        {
            Console.WriteLine($"An error occurred: {er.Message}");
        }


    }
    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    private static ServiceProvider CreateServices()
    {
        try
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .AddSingleton<IConventionSet>(new DefaultConventionSet("jiji", null))
                .ConfigureRunner(rb => rb
                    // Add postgresql support to FluentMigrator
                    .AddPostgres11_0()
                    // Set the connection string
                    // SEARCH_PATH: ;
                    .WithGlobalConnectionString("Host=192.168.1.10;Port=5432;Database=test;User Id=postgres;Password=PostgresDB@dm1n;SearchPath=jiji;")
                    .WithRunnerConventions(new MigrationRunnerConventions())
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(AddLogTable1).Assembly).For.Migrations())

                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
               // Build the service provider
               .BuildServiceProvider(false);
        }
        catch (Exception er)
        {
            Console.WriteLine($"An error occurred: {er.Message}");
            return new ServiceCollection().BuildServiceProvider(false);
        }
    }

    /// <summary>
    /// Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner with the custom connection
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // runs all pending migrations to upgrade the database schema.

        runner.MigrateUp();
    }

}


