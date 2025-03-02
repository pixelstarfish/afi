using Afi.CustomerPortal.Entities.Dto;
using Afi.CustomerPortal.Services.Configuration;
using Afi.CustomerPortal.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Configure global error handling.
builder.Logging.AddConsole();
// Configure auto validation.


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure parameter validation.
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CustomerRegistration>, CustomerRegistrationValidator>();

// Configure abstracted data logic layer.
builder.Services.ConfigureDataLayer(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        var logger = context.Features.Get<IExceptionHandlerPathFeature>();
        if (logger != null)
        {
            // Log any exceptions raised.
            app.Logger.LogError(logger.Error, null);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An error occurred.");
        }
    });
});
app.UseStatusCodePages();

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