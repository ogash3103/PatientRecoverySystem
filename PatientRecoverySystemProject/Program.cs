// File: PatientRecoverySystemProject/Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

// Quyidagi using’larni tekshiring: 
// PatientRecoverySystemProject.Data ichida ApplicationDbContext mavjud bo‘lishi kerak
using PatientRecoverySystemProject.Data;
// PatientRecoverySystemProject.Services ichida DiagnosisService va grpc‐client klasslari mavjud bo‘lishi kerak
using PatientRecoverySystemProject.Services;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------------------------------------------
// 1) REST API Controllers uchun servislarni ro’yxatga o‘tkazish
// -------------------------------------------------------------------------------------------------
builder.Services.AddControllers();

// -------------------------------------------------------------------------------------------------
// 2) Swagger/OpenAPI sozlash (agar lozim bo’lsa)
// -------------------------------------------------------------------------------------------------
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

// -------------------------------------------------------------------------------------------------
// 3) DbContext (SQL Server) sozlash
//    appsettings.json ichida quyidagicha bo‘lishi kerak:
//    "ConnectionStrings": {
//        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PatientRecoveryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
//    }
// -------------------------------------------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------------------------------------------------------------------------------------
// 4) Biznes‐logik (DiagnosisService) va boshqa “pattern‐asosidagi” servislarni ro’yxatga o‘tkazish
//    - DiagnosisService: Symptoms ro‘yxatiga qarab tavsiya qaytaradi va DB’ga saqlaydi
//    - Agar qo‘shimcha biznes servislari bo‘lsa, ularni ham shu yerga kiriting:
//      builder.Services.AddScoped<MyBusinessService>();
// -------------------------------------------------------------------------------------------------
builder.Services.AddScoped<DiagnosisService>();

// -------------------------------------------------------------------------------------------------
// 5) gRPC‐mikroxizmatlariga murojaat qilish uchun GRPC‐klientlarni (singleton) ro’yxatga o‘tkazish
//    – DiagnoseGrpcClient   (DiagnoseService gRPC serveriga ulanadi)
//    – MonitoringGrpcClient (MonitoringService gRPC serveriga ulanadi)
//    – RehabilitationGrpcClient (RehabilitationService gRPC serveriga ulanadi)
// -------------------------------------------------------------------------------------------------
builder.Services.AddSingleton<DiagnoseGrpcClient>();
builder.Services.AddSingleton<MonitoringGrpcClient>();
builder.Services.AddSingleton<RehabilitationGrpcClient>();

var app = builder.Build();

// -------------------------------------------------------------------------------------------------
// 6) Development muhiti uchun Swagger va Developer exception page
// -------------------------------------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PatientRecoverySystem API v1");
    });
}

// -------------------------------------------------------------------------------------------------
// 7) Middleware: HTTPS redirection va Authorization (agar kerak bo‘lsa)
// -------------------------------------------------------------------------------------------------
app.UseHttpsRedirection();
app.UseAuthorization();

// -------------------------------------------------------------------------------------------------
// 8) REST endpointlarni xaritada bog‘lash
//    (Controller-laringiz [ApiController] attributi bilan belgilangan bo‘lishi kerak)
// -------------------------------------------------------------------------------------------------
app.MapControllers();

// -------------------------------------------------------------------------------------------------
// 9) Test uchun oddiy GET endpoint (health check)
// -------------------------------------------------------------------------------------------------
app.MapGet("/", () => "🌐 PatientRecoverySystem Gateway: REST⇒gRPC ishlamoqda!");

// -------------------------------------------------------------------------------------------------
// 10) Ilovani ishga tushirish
// -------------------------------------------------------------------------------------------------
app.Run();
