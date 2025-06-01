// Program.cs
using DiagnoseService.Services;                // gRPC: DiagnoseServiceImpl
using Microsoft.EntityFrameworkCore;
using MonitoringService.Services;               // gRPC: MonitoringServiceImpl
using PatientRecoverySystemProject.Data;
using PatientRecoverySystemProject.Services;     // DiagnosisService, MonitoringGrpcClient
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1) gRPC server xizmatlarini ro‘yxatdan o‘tkazish:
builder.Services.AddGrpc();

// 2) REST API Controllers services
builder.Services.AddControllers();

// 3) Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PatientRecoverySystem API",
        Version = "v1",
        Description = "REST endpoints for Patient Recovery System"
    });
});

// 4) DbContext (SQL Server) sozlash (appsettings.json ichida “DefaultConnection” bo‘lishi lozim):
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 5) Bizning local “services” (dependency injection):
builder.Services.AddScoped<DiagnosisService>();         // “business‐logic” sinf
builder.Services.AddSingleton<MonitoringGrpcClient>();   // gRPC client (singleton)
builder.Services.AddSingleton<RehabilitationGrpcClient>();


var app = builder.Build();

// 6) Development muhiti uchun Swagger, DeveloperExceptionPage
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PatientRecoverySystem API v1");
    });
}

// 7) Middleware: HTTPS, Authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// 8) gRPC va REST endpointlarni xaritada bog‘lash
app.MapGrpcService<MonitoringServiceImpl>();
app.MapGrpcService<DiagnoseServiceImpl>();
app.MapControllers();   // [ApiController] bilan belgilangan REST endpointlar

// 9) Asosiy test endpoint (ixtiyoriy)
app.MapGet("/", () => "🌐 PatientRecoverySystem Gateway: gRPC + Web API ishlamoqda!");

// 10) Ilovani ishga tushirish
app.Run();
