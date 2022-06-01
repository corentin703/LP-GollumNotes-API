using System.Text.Json;
using System.Text.Json.Serialization;
using CoverotNimorin.GollumChat.Server.Configuration;
using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts;
using CoverotNimorin.GollumChat.Server.Middleware;
using CoverotNimorin.GollumChat.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AppConfiguration>(
    builder.Configuration.GetSection("AppSettings")
);

builder.Services.AddNpgsql<GollumNotesContext>(
    builder.Configuration.GetConnectionString("Default")
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();

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