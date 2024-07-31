using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    // Représente un itinéraire
    public class Itineraire
    {
        // Clé primaire de l'itinéraire
        [Key]
        public required string CodeItineraire { get; set; }

        // Lieu de départ de l'itinéraire
        public string LieuDepart { get; set; } = string.Empty;

        // Lieu d'arrivée de l'itinéraire
        public string LieuArrivee { get; set; } = string.Empty;

        // Heure de départ de l'itinéraire
        public string HeureDepart { get; set; } = string.Empty;

        // Jour de départ de l'itinéraire
        public string JourDepart { get; set; } = string.Empty;
    }
}
