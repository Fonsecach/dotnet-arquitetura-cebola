using Ecommerce.Infrastructure.CrossCutting.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RavenDbStettings>(builder.Configuration.GetSection("RavenDbSettings"));
builder.Services.AddRavenDb();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMappers();
builder.Services.AddControllers();
builder.Services.AddHealthChecks()
                .AddRavenDB( setup =>
                {
                    setup.Database="Ecommerce";
                    setup.Urls= ["http://localhost:8080"];
                }
                );


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
