using CoverotNimorin.GollumChat.Server.Configuration;
using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Contracts.Services;
using CoverotNimorin.GollumChat.Server.Middleware;
using CoverotNimorin.GollumChat.Server.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Services;

var builder = WebApplication.CreateBuilder(args);

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    
    app.UseDeveloperExceptionPage();
}

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
}

app.UseHttpsRedirection();

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

    return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
}