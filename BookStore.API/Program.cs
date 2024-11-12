using BookStore.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

/* Inject All Service here*/
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

// Configure Swagger with JWT Bearer Authentication
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Configure CORS to allow ngrok connections (replace with your ngrok subdomain)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNgrok",
        builder => builder
            .WithOrigins("https://glad-lion-holy.ngrok-free.app") // Replace with your actual ngrok subdomain
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Enable Swagger only in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API V1");
        c.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection(); // Only enforce HTTPS in production
}

app.UseCors("AllowNgrok"); // Apply CORS policy for ngrok

app.UseAuthorization();

app.MapControllers();

app.Run();
