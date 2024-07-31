using Microsoft.EntityFrameworkCore;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajouter des services au conteneur.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Configure les services pour les contrôleurs
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EmailService>();

var app = builder.Build();

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
