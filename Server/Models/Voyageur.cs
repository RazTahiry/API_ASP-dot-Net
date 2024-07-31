using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    // Représente un voyageur
    public class Voyageur
    {
        // Clé primaire de l'entité Voyageur, unique pour chaque voyageur
        [Key]
        public int NumTicket { get; set; }

        // Adresse email du voyageur
        public string EmailVoyageur { get; set; } = string.Empty;

        // Nom du voyageur
        public string NomVoyageur { get; set; } = string.Empty;

        // Date de départ du voyage
        public string DateDepart { get; set; } = string.Empty;

        // Nombre de places réservées par le voyageur
        public int NbPlace { get; set; } = 1;

        // Clé étrangère faisant référence à la catégorie associée au voyageur
        public required string CodeCategorie { get; set; } = null!;
    }
}
