using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    // Représente une catégorie dans le train
    public class Categorie
    {
        // Clé primaire de la catégorie
        [Key]
        public required string CodeCategorie { get; set; }

        // Libellé ou nom de la catégorie
        public string LibelleCategorie { get; set; } = string.Empty;

        // Nombre de places supportées par la catégorie
        public int NbPlaceSupporte { get; set; }

        // Frais associés à la catégorie
        public double Frais { get; set; }

        // Immatriculation associée à la catégorie
        public required string Immatriculation { get; set; } = null!;
    }
}
