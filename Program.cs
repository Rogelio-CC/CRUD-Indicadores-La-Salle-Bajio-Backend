using KPIBackend.Application.Services;
using KPIBackend.Data;
using KPIBackend.Infrastructure.Repositories;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Text;


DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Indicadores Salle", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
                      Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
                      Example: 'Bearer 12345abcdef'<br /> <br />",
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

string connectionString;
var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

connectionString = ConvertPostgresUrlToConnectionStringInProgram(connectionUrl!);

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key no configurada");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    };
});



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IIndicadorRepository, IndicadorRepository>();
builder.Services.AddScoped<IFacultadRepository, FacultadRepository>();
builder.Services.AddScoped<IDirectrizRepository, DirectrizRepository>();
builder.Services.AddScoped<IPeriodoEscolarRepository, PeriodoEscolarRepository>();
builder.Services.AddScoped<ICarreraRepository, CarreraRepository>();
builder.Services.AddScoped<IEstrategiaRepository, EstrategiaRepository>();
builder.Services.AddScoped<IActividadRepository, ActividadRepository>();
builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
builder.Services.AddScoped<IBaseRepository<Rol>, BaseRepository<Rol>>();
builder.Services.AddScoped<IBaseRepository<GrupoIndicadores>, BaseRepository<GrupoIndicadores>>();
builder.Services.AddScoped<IEvidenciaRepository, EvidenciaRepository>();
builder.Services.AddScoped<IBaseRepository<EventoCalendario>, BaseRepository<EventoCalendario>>();

// Leer orígenes permitidos desde variable de entorno (comma-separated) o fallback a config / valor por defecto.
var allowedOriginsValue = builder.Configuration["Cors:AllowedOrigins"];

var allowedOrigins = allowedOriginsValue!
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(o => o.Trim().TrimEnd('/'))
    .ToArray();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});



var app = builder.Build();

app.UseCors("AllowBlazor");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
Console.WriteLine("✅ API corriendo en: " + app.Urls.FirstOrDefault());
app.Run();

static string ConvertPostgresUrlToConnectionStringInProgram(string url)
{
    var uri = new Uri(url);
    var userInfo = uri.UserInfo.Split(':', 2);
    var user = Uri.UnescapeDataString(userInfo[0]);
    var pass = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty;
    var host = uri.Host;
    var port = uri.IsDefaultPort ? 5432 : uri.Port;
    var database = uri.AbsolutePath?.TrimStart('/') ?? string.Empty;

    // parse query into dictionary
    var queryDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    var rawQuery = uri.Query?.TrimStart('?') ?? string.Empty;
    if (!string.IsNullOrEmpty(rawQuery))
    {
        foreach (var part in rawQuery.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var kv = part.Split('=', 2);
            var k = Uri.UnescapeDataString(kv[0]);
            var v = kv.Length > 1 ? Uri.UnescapeDataString(kv[1]) : string.Empty;
            queryDict[k] = v;
        }
    }

    var builder = new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = port,
        Username = user,
        Password = pass,
        Database = database,
    };

    if (queryDict.TryGetValue("sslmode", out var sslMode))
    {
        if (sslMode.Equals("disable", StringComparison.OrdinalIgnoreCase))
            builder.SslMode = SslMode.Disable;
        else if (sslMode.Equals("require", StringComparison.OrdinalIgnoreCase) || sslMode.Equals("prefer", StringComparison.OrdinalIgnoreCase))
            builder.SslMode = SslMode.Require;
        else if (sslMode.Equals("verify-ca", StringComparison.OrdinalIgnoreCase) || sslMode.Equals("verify-full", StringComparison.OrdinalIgnoreCase))
            builder.SslMode = SslMode.VerifyFull;
    }
    else
    {
        builder.SslMode = SslMode.Require;
    }

    return builder.ToString();
}
