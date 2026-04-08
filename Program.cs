using KPIBackend.Application.Services;
using KPIBackend.Data;
using KPIBackend.Models;
using KPIBackend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

//Lectura local del archivo .env (no es necesario en entorno de producción)
DotNetEnv.Env.Load();

// Punto de entrada de la API.
// Este archivo configura los servicios principales de la aplicación,
// incluyendo autenticación JWT, conexión a base de datos,
// repositorios, Swagger y políticas CORS.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Configura Swagger para generar la documentación interactiva de la API.
// Incluye soporte para autenticación mediante Bearer Token (JWT)
// y carga los comentarios XML generados a partir del código.
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
    // Documentacion prueba
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Obtiene la cadena de valor de la base de datos de SQL server desde una variable
// de entorno para evitar almacenar credenciales sensibles
// directamente en el código fuente. Nota: es importante poner correctamente
// DATABASE_CONNECTION_STRING en archivo .env o
// para que se pueda identificar la cadena.
var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

// Se obtiene el valor del Key del token JWT desde las variables de entorno.
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key no configurada");


// Configura la autenticación basada en JWT.
// Los tokens son utilizados para proteger los endpoints
// y validar la identidad de los usuarios autenticados.
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


// Registra el contexto de base de datos utilizando Entity Framework Core
// y el proveedor SQL server.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


// Registro de repositorios y servicios de la aplicación. 
// Se utiliza inyección de dependencias para proporcionar acceso
// a los repositorios encargados de interactuar con la base de datos.
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
builder.Services.AddScoped<IArchivoPoliticasRepository, ArchivoPoliticasRepository>();


// Configura la política CORS para permitir la comunicación
// entre el frontend (Blazor) y el backend.
// Se lee los orígenes permitidos desde variable de entorno.
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



// Construye la aplicación y configura el pipeline de middleware
// que procesará las solicitudes HTTP.
var app = builder.Build();


// Configuración del pipeline HTTP:
// - CORS
// - Swagger
// - Redirección HTTPS
// - Autenticación
// - Autorización
// - Mapeo de controladores
app.UseCors("AllowBlazor");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
Console.WriteLine("✅ API corriendo en: " + app.Urls.FirstOrDefault());
app.Run();