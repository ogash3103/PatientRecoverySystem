using MonitoringService.Services;

var builder = WebApplication.CreateBuilder(args);

// gRPC qo‘shish
builder.Services.AddGrpc();

var app = builder.Build();

// gRPC endpointni map qilamiz
app.MapGrpcService<MonitoringServiceImpl>();

app.MapGet("/", () => "Use a gRPC client to communicate with MonitoringService.");

app.Run();
