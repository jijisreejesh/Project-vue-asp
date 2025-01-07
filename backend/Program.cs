
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
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ICustomerService ,CustomerService>();
            builder.Services.AddTransient<ISalesService,SalesService>();
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
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                                builder =>
                                {
                                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                                });
            });
            
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.MapControllers();

            using var scope = app.Services.CreateScope();
            UpdateDatabase(scope.ServiceProvider);

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

        // Execute the migrations
        runner.MigrateUp();
    }

}


