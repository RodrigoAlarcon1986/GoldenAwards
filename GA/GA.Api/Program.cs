using GA.Api.Middlewares;
using GA.Api.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add modules
builder.Services.AddGaModule(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

// Init modules
await app.InitGaModuleAsync();

await app.RunAsync();
