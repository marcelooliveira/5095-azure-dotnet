using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using VollMed.Web.Data;
using VollMed.Web.Interfaces;
using VollMed.Web.Repositories;
using VollMed.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>((options) => {
    options
            .UseSqlServer(builder.Configuration["ConnectionStrings:VollMedDB"],
                b => b.MigrationsAssembly("VollMed.WebAPI"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();
builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
builder.Services.AddTransient<IMedicoService, MedicoService>();
builder.Services.AddTransient<IConsultaService, ConsultaService>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options => {
        builder.Configuration.Bind("AzureAd", options);
    },
    options => {
        builder.Configuration.Bind("AzureAd", options);
    });
builder.Services.AddAuthorization();


// Habilita o Application Insights (telemetria automática)
builder.Services.AddApplicationInsightsTelemetry();

// Configura telemetria
builder.Services.Configure<TelemetryConfiguration>((config) =>
{
    config.TelemetryChannel.DeveloperMode = true; // flush imediato
});

// Habilita o provedor de logging do Application Insights
builder.Logging.AddApplicationInsights();

builder.Services.AddOpenTelemetry()
    .UseAzureMonitor(o =>
    {
        o.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
    });


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
