using BusinessLogicLayer;
using DataAccessLayer;
using FluentValidation.AspNetCore;
using ProductsService.API.APIEndpoints;
using ProductsService.API.MIddleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

//add model binder to read values from json
builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.Converters.Add(new
        JsonStringEnumConverter());
});

//add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

//cors
app.UseCors();

//swagger
app.UseSwagger();
app.UseSwaggerUI();


//auth
app.UseHttpsRedirection();
app.UseAuthentication(); ;
app.UseAuthorization();


app.MapControllers();
app.MapProductAPIEndpoints();

app.Run();
