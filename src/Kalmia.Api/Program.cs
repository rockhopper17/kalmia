using Kalmia.Core.Interfaces;
using Kalmia.Core.Services;
using Kalmia.Data;
using Kalmia.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// custom add for enums
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

builder.Services.AddDbContext<KalmiaDbContext>(opt =>
{
    var baseConnection = builder.Configuration.GetConnectionString("BaseSqlServer");
    var dbName = builder.Configuration["DatabaseName"];
    opt.UseSqlServer($"{baseConnection};Database={dbName}");
});

builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityService, ActivityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<KalmiaDbContext>();
    await SeedData.SeedAsync(dbContext);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
