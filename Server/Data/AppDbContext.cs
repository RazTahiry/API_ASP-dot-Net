using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    // Contexte de la base de données pour l'application
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        // Propriétés DbSet pour accéder aux tables de la base de données
        public DbSet<Voyageur> Voyageurs { get; set; }
        public DbSet<Itineraire> Itineraires { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Train> Trains { get; set; }

        // Configuration des relations entre les entités
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration des relations entre Itineraire et Train
            modelBuilder.Entity<Itineraire>()
                .HasMany<Train>() // Un itinéraire peut avoir plusieurs trains
                .WithOne() // Un train appartient à un itinéraire
                .HasForeignKey(i => i.CodeItineraire) // La clé étrangère est CodeItineraire dans Train
                .IsRequired(); // La relation est requise (non nullable)

            // Configuration des relations entre Train et Categorie
            modelBuilder.Entity<Train>()
                .HasMany<Categorie>() // Un train peut avoir plusieurs catégories
                .WithOne() // Une catégorie appartient à un train
                .HasForeignKey(t => t.Immatriculation) // La clé étrangère est Immatriculation dans Categorie
                .IsRequired(); // La relation est requise (non nullable)

            // Configuration des relations entre Categorie et Voyageur
            modelBuilder.Entity<Categorie>()
                .HasMany<Voyageur>() // Une catégorie peut avoir plusieurs voyageurs
                .WithOne() // Un voyageur appartient à une catégorie
                .HasForeignKey(t => t.CodeCategorie) // La clé étrangère est CodeCategorie dans Voyageur
                .IsRequired(); // La relation est requise (non nullable)
        }
    }
}
