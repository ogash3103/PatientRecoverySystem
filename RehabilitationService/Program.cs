using RehabilitationService.Services;

var builder = WebApplication.CreateBuilder(args);

// gRPC xizmati qo‘shiladi
builder.Services.AddGrpc();

var app = builder.Build();

// gRPC endpointini xaritada ko‘rsatish
app.MapGrpcService<RehabilitationServiceImpl>();
app.MapGet("/", () => "📄 RehabilitationService – gRPC ishlayapti");

app.Run();
