using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajouter des services au conteneur.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Configure les services pour les contrôleurs
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EmailService>();

// Configuration des paramètres JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
string? key = jwtSettings["Key"];
string? issuer = jwtSettings["Issuer"];
string? audience = jwtSettings["Audience"];

if (key == null || issuer == null || audience == null)
{
    throw new InvalidOperationException("Les paramètres JWT ne sont pas configurés correctement.");
}

// Ajouter l'authentification JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

// Configurer les points de terminaison pour les contrôleurs
app.MapControllers();

// Configurer le pipeline de requêtes HTTP.
if (app.Environment.IsDevelopment())
{
    // Activer Swagger et l'interface utilisateur Swagger en environnement de développement
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activer la redirection HTTPS
app.UseHttpsRedirection();

// Démarrer l'application
app.Run();
