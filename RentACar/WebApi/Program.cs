using Application;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Persistance; 

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApplicationServices()
    .AddPersistanceService(builder.Configuration);

builder.Services.AddControllers().AddNewtonsoftJson(cfg =>
{
    cfg.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
