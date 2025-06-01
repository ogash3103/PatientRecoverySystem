using DiagnoseService.Services;

var builder = WebApplication.CreateBuilder(args);

// gRPC tizimini qo‘shamiz
builder.Services.AddGrpc();

var app = builder.Build();

// gRPC endpointni map qilamiz
app.MapGrpcService<DiagnoseServiceImpl>();

// Oddiy HTTP GET asosiy root so‘rovga javob beradi
app.MapGet("/", () => "Use a gRPC client to communicate with DiagnoseService.");

app.Run();
