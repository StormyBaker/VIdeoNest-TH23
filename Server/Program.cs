using VideoNestServer.Settings;
using VideoNestServer.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<Secrets>(builder.Configuration.GetSection("Secrets"));
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddTransient<TokenManager, TokenManager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins(new[] { "http://localhost:3000", "https://www.videonest.us", "https://videonest.us"})
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Allow credentials
    });
});

builder.Services.AddAuthentication()
    .AddScheme<SessionTokenAuthenticationSchemeOptions, SessionTokenAuthenticationSchemeHandler>(
        "JWT",
        opts => { }
    );

builder.Services.AddControllers().AddNewtonsoftJson();


var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
