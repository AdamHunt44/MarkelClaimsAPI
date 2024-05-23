using MarkelClaimsAPI.Infrastructure;
using MarkelClaimsAPI.Infrastructure.Context;
using MarkelClaimsAPI.Services;
using MarkelClaimsAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IMarkelDbContext, MarkelDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"))
);

builder.Services.AddScoped<IMarkelDbContext, MarkelDbContext>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IMarkelDbContext>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
