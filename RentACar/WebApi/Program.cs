using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

//builder.Services.AddStackExchangeRedisCache(opt =>
//{
//    opt.Configuration = "localhost:6379";
//});

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddApplicationServices()
    .AddPersistanceService(builder.Configuration);

builder.Services.AddControllers(); //.AddNewtonsoftJson(cfg =>
//{
//    cfg.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

//});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//if (!app.Environment.IsDevelopment())
    app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
