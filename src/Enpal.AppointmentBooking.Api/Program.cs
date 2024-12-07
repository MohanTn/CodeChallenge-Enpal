using System.Text.Json;
using Enpal.AppointmentBooking.Api.Mappers;
using Enpal.AppointmentBooking.Application.Interface;
using Enpal.AppointmentBooking.Application.Services;
using Enpal.AppointmentBooking.Core.Interface;
using Enpal.AppointmentBooking.Infrastructure;
using Enpal.AppointmentBooking.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var database = builder.Configuration.GetSection("Database").Get<DatabaseSetting>();

// Database connection
builder.Services.AddDbContext<ApplicationDbContext>(
    (serviceProvider, option) =>
    {
        option.UseNpgsql(database.GetConnectionString());
    }
);

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddAutoMapper(typeof(ApiModelToDtoMapperProfile).Assembly);
builder.Services.AddScoped<ICalendarQueryService, CalendarQueryService>();
builder.Services.AddScoped<IApplicationDbContext>(x => x.GetService<ApplicationDbContext>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc(
        "1.0",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Enpal: Appointment Booking",
            Version = "1.0",
            Description = "Take Home Challenge Enpal",
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/1.0/swagger.json", "Appointment Booking");
        option.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
