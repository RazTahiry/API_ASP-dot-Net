using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    // Représente un train
    public class Train
    {
        // Clé primaire de l'entité Train, unique pour chaque train
        [Key]
        public required string Immatriculation { get; set; }

        // Clé étrangère faisant référence à l'itinéraire associé au train
        public required string CodeItineraire { get; set; } = null!;
    }
}
