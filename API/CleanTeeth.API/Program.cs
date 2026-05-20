using CleanTeeth.API.Endpoints;
using CleanTeeth.API.Middlewares;
using CleanTeeth.Application;
using CleanTeeth.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Services
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAplicationServices();
builder.Services.AddPersistenceServices();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region Middlewares
app.UseErrorExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

#endregion

#region Route endpoints
RouteGroupBuilder api = app.MapGroup("/api");

api.MapGroup("/consulting-rooms").MapConsultingRooms();
#endregion

app.Run();
