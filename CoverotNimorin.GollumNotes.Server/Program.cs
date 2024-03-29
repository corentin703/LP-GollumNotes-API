using CoverotNimorin.GollumNotes.Server.Configuration;
using CoverotNimorin.GollumNotes.Server.Contexts;
using CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Contracts.Services;
using CoverotNimorin.GollumNotes.Server.Middleware;
using CoverotNimorin.GollumNotes.Server.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    IConfigurationSection kestrelSection = builder.Configuration.GetSection("Kestrel");
    if (builder.Environment.EnvironmentName == "Heroku")
    {
        string? portStr = System.Environment.GetEnvironmentVariable("PORT");
        if (!int.TryParse(portStr, out int port))
            port = 80;
            
        serverOptions.ListenAnyIP(port);
    }
    else
        serverOptions.Configure(kestrelSection);
});

// Add services to the container.

builder.Services.Configure<AppConfiguration>(
    builder.Configuration.GetSection("AppSettings")
);

string databaseConnexionString = builder.Configuration.GetConnectionString("Default");
if (builder.Environment.EnvironmentName == "Heroku")
    databaseConnexionString = GetHerokuConnectionString();

builder.Services.AddNpgsql<GollumNotesContext>(databaseConnexionString);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IPictureRepository, PictureRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IPictureService, PictureService>();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    
    app.UseDeveloperExceptionPage();
}

ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Running on environment {Env}", app.Environment.EnvironmentName);    

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

using (var scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;

    GollumNotesContext context = services.GetRequiredService<GollumNotesContext>();
    context.Database.EnsureCreated();
    logger.LogInformation("Database up to work !");
}

// app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<JwtAuthMiddleware>();

app.MapControllers();

app.Run();

string GetHerokuConnectionString()
{
    string? connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (connectionUrl == null)
        throw new Exception("No Database URL for Heroku environment");

    Uri databaseUri = new(connectionUrl);

    string db = databaseUri.LocalPath.TrimStart('/');
    string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

    return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};sslmode=Require;TrustServerCertificate=True;";
}