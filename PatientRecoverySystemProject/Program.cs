// Program.cs (PatientRecoverySystemProject)

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PatientRecoverySystemProject.Data;
using PatientRecoverySystemProject.Services;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------------------------------------------
// 1) REST API Controllers
// -------------------------------------------------------------------------------------------------
builder.Services.AddControllers();

// -------------------------------------------------------------------------------------------------
// 2) Swagger/OpenAPI
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
//    appsettings.json ichida “DefaultConnection” bo‘lishi kerak:
//    "ConnectionStrings": {
//        "DefaultConnection": "Server=...;Database=...;User Id=...;Password=...;TrustServerCertificate=True"
//    }
// -------------------------------------------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------------------------------------------------------------------------------------
// 4) gRPC‐mikroxizmatlariga murojaat qilish uchun GRPC‐klientlarni (singleton) ro‘yxatga olish
//    – DiagnoseGrpcClient   (DiagnoseService gRPC)
//    – MonitoringGrpcClient (MonitoringService gRPC)
//    – RehabilitationGrpcClient (RehabilitationService gRPC)
// -------------------------------------------------------------------------------------------------
builder.Services.AddSingleton<DiagnoseGrpcClient>();
builder.Services.AddSingleton<MonitoringGrpcClient>();
builder.Services.AddSingleton<RehabilitationGrpcClient>();

// -------------------------------------------------------------------------------------------------
// 5) “Pattern‐i” bo‘yicha, agar Gateway’da boshqa “biznes mantiq” (Business Logic) sinflaringiz bo‘lsa, 
//    ularni ham shu yerda ro‘yxatga o‘tkazishingiz mumkin, masalan:
//    builder.Services.AddScoped<MyBusinessService>();
// -------------------------------------------------------------------------------------------------

var app = builder.Build();

// -------------------------------------------------------------------------------------------------
// 6) Development muhiti uchun Swagger va Exception page
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
// 7) Middleware: HTTPS, Authorization
// -------------------------------------------------------------------------------------------------
app.UseHttpsRedirection();
app.UseAuthorization();

// -------------------------------------------------------------------------------------------------
// 8) REST endpointlarni xaritada bog‘lash
//    (Controller-laringiz [ApiController] bilan belgilangan bo‘lishi kerak)
// -------------------------------------------------------------------------------------------------
app.MapControllers();

// -------------------------------------------------------------------------------------------------
// 9) Test uchun “health check” yoki oddiy GET
// -------------------------------------------------------------------------------------------------
app.MapGet("/", () => "🌐 PatientRecoverySystem Gateway: REST⇒gRPC ishlamoqda!");

// -------------------------------------------------------------------------------------------------
// 10) Ilovani ishga tushirish
// -------------------------------------------------------------------------------------------------
app.Run();
