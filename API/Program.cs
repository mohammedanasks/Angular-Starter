
using API.Entities;
using API.Services.Leaves;
using API.Services.Masters;
using API.Services.Report;
using API.Services.Salary;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var myAllowspecificOrigins = "_myAllowspecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Defaultconnection"),
        sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });

});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowspecificOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:50905")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddScoped<EmployeeServices>();
builder.Services.AddScoped<DepartmentServices>();
builder.Services.AddScoped<LeaveServices>();
builder.Services.AddScoped<SalaryServices>();
builder.Services.AddScoped<ReportsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowspecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
