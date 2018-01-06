using System;
using System.Data.SqlClient;
using Autofac;
using CentralBackup.Migrations;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;

namespace CentralBackup.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<Core.Repositories.BackupContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddHangfire(configuration => configuration.UseSqlServerStorage(Configuration.GetConnectionString("Default")));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            DatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions {
                Authorization = new [] { new HangfireAuthFilter() }
            });

            RegisterJobs(app);
        }

        private void DatabaseMigration()
        {
            var migrationsAssembly = typeof(CreateJobsTable001).Assembly;

            using (var connection = new SqlConnection(Configuration.GetConnectionString("Default")))
            {
                var databaseProvider = new MssqlDatabaseProvider(connection);
                var migrator = new SimpleMigrator(migrationsAssembly, databaseProvider);

                migrator.Load();
                migrator.MigrateToLatest();
            }
        }

        private void RegisterJobs(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var jobRepository = scope.ServiceProvider.GetService<Core.Interfaces.Repositories.IJobRepository>();

                foreach (var job in jobRepository.LoadAll())
                {
                    RecurringJob.AddOrUpdate(job.HangfireJobId.ToString(), () => scope.ServiceProvider.GetService<Execution.JobExecutor>().Execute(job.Id), job.Cron, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
                }
            }
        }
    }
}
