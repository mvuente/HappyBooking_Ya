
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
}); 

builder.Services.AddFluentValidationAutoValidation()       //Подключен Fluent validator
               .AddFluentValidationClientsideAdapters()
               .AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddSingleton<IEventService, BasicEventService>(); //Singleton, тк используется коллекция событий in memory
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();


var app = builder.Build();

app.UseMiddleware<GlobalExceptionsHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
