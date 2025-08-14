using DB.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Servicios.Interfaces;
using Servicios.Servicios;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de CORS para permitir solicitudes desde tu frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  // Aseg�rate de que este sea el origen correcto de tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();  // Permite el uso de credenciales (como cookies o encabezados de autorizaci�n)
    });
});

// Configuraci�n de autenticaci�n JWT
builder.Services.AddAuthentication(opcionesDeAutenticacion =>
{
    opcionesDeAutenticacion.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opcionesDeAutenticacion.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false, 
        ValidateAudience = false, 
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? string.Empty)
        )
    };
});

// Servicios de autorizaci�n
builder.Services.AddAuthorization();

// Configuraci�n de servicios y DbContext
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { // Autenticar en Swagger
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Persicuf", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Encabezado de autorizaci�n JWT utilizando el esquema Bearer. \r\n\r\n
                          Ingresamos la palabra 'Bearer', luego un espacio y el token.
                          \r\n\r\nPor ejemplo: 'Bearer fedERGeWefrt5t45e5g5g4f643333uyhr'",
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
builder.Services.AddHttpClient();

// Configuraci�n de la base de datos (en este caso, usando PostgreSQL)
builder.Services.AddDbContext<PersicufContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));  // Aseg�rate de que la cadena de conexi�n sea correcta

// Inyecci�n de dependencias de los servicios
builder.Services.AddScoped<IColorServicio, ColorServicio>();
builder.Services.AddScoped<ICamperaServicio, CamperaServicio>();
builder.Services.AddScoped<ICorteCuelloServicio, CorteCuelloServicio>();
builder.Services.AddScoped<IDomicilioServicio, DomicilioServicio>();
builder.Services.AddScoped<IImagenServicio, ImagenServicio>();
builder.Services.AddScoped<ILargoServicio, LargoServicio>();
builder.Services.AddScoped<ILocalidadServicio, LocalidadServicio>();
builder.Services.AddScoped<IMangaServicio, MangaServicio>();
builder.Services.AddScoped<IMaterialServicio, MaterialServicio>();
builder.Services.AddScoped<IPantalonServicio, PantalonServicio>();
builder.Services.AddScoped<IPedidoPrendaServicio, PedidoPrendaServicio>();
builder.Services.AddScoped<IPedidoServicio, PedidoServicio>();
builder.Services.AddScoped<IPermisoServicio, PermisoServicio>();
builder.Services.AddScoped<IPrendaServicio, PrendaServicio>();
builder.Services.AddScoped<IProvinciaServicio, ProvinciaServicio>();
builder.Services.AddScoped<IRemeraServicio, RemeraServicio>();
builder.Services.AddScoped<IRubroServicio, RubroServicio>();
builder.Services.AddScoped<ITalleAlfabeticoServicio, TalleAlfabeticoServicio>();
builder.Services.AddScoped<ITalleNumericoServicio, TalleNumericoServicio>();
builder.Services.AddScoped<IUbicacionServicio, UbicacionServicio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<IZapatoServicio, ZapatoServicio>();
builder.Services.AddScoped<IJWT, JWT>();
builder.Services.AddScoped<IEnvioAPIServicio, EnvioAPIServicio>();

var app = builder.Build();

// Migraci�n autom�tica de la base de datos (si es necesario)
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<PersicufContext>();
//    context.Database.Migrate();  // Aplica migraciones pendientes
//}

// Configuraci�n de Swagger para desarrollo (si est� en el entorno de desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Uso de CORS y autenticaci�n
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

app.Run();
