using System;
using System.Linq;
using DAL;
using DAL.Repositories;
using Domain.Configs;
using Domain.Interfaces;
using Domain.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services;
using Services.Helper;
using Services.Interface;

namespace Chat.Application.Web.Service;

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

        services.Configure<WebAppSettings>(Configuration);
        services.AddScoped<IBotServiceHttpHelper, BotServiceHttpHelper>();
        services.AddSingleton<IQueueConnection, QueueConnectionHelper>();
        services.AddSingleton<IQueueConsumerService, QueueConsumerService>();
        services.AddScoped<IChatHubService, ChatHubService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat.Application.Web.Service", Version = "v1" });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });

        services.AddDbContextPool<ChatDbContext>(builder =>
        {
            var connectionString = Configuration.GetConnectionString("Database");
            builder.UseSqlServer(connectionString);
        });

        //password require for making process simple for demo purpose only
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 0;
        });

        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ChatDbContext>();

        services.AddTransient<ExceptionHandlingMiddleware>();
        services.AddSignalR();
        services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(host => true)
                .AllowCredentials();
        }));
        services.AddHttpContextAccessor();
        services.AddAuthentication();
        services.AddAuthorization();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
        }

        serviceProvider.GetService<IQueueConsumerService>()?.StartListening();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("CorsPolicy");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<ChatHubService>("/chatNotificationHub");
        });
    }
}