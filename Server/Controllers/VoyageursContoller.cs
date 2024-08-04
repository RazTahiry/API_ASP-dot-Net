using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // Contrôleur pour gérer les opérations sur les voyageurs
    public class voyageursController(AppDbContext context) : ControllerBase
    {
        // Initialisation du contexte de la base de données
        private readonly AppDbContext _context = context;

        // Endpoint pour obtenir tous les voyageurs
        [HttpGet]
        public async Task<IEnumerable<Voyageur>> GetVoyageurs()
        {
            // Récupère tous les voyageurs sans suivi (pour optimiser les performances)
            var voyageurs = await _context.Voyageurs.AsNoTracking().ToListAsync();

            return voyageurs;
        }

        // Endpoint pour créer une nouvelle réservation
        [HttpPost]
        public async Task<IActionResult> Create(Voyageur voyageur)
        {
            try
            {
                // Vérifie si le modèle est valide
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Ajoute le nouveau voyageur (réservation) à la base de données
                await _context.AddAsync(voyageur);

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la sauvegarde a réussi
                if (result > 0)
                    return Ok("Réservation réussie !");

                return BadRequest("Échec de la réservation.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de la réservation : {e.Message}");
                throw;
            }
        }

        // Endpoint pour obtenir un voyageur spécifique par son numéro de ticket
        [HttpGet("{numTicket}")]
        public async Task<ActionResult<Voyageur>> GetVoyageur(int numTicket)
        {
            // Recherche le voyageur dans la base de données par son numéro de ticket
            var voyageur = await _context.Voyageurs.FindAsync(numTicket);

            if (voyageur is null)
                return StatusCode(404, "Réservation non trouvé.");

            return Ok(voyageur);
        }

        // Endpoint pour annuler une réservation par le numéro de ticket
        [HttpDelete("{numTicket}")]
        public async Task<IActionResult> Delete(int numTicket)
        {
            try
            {
                // Recherche le voyageur dans la base de données par son numéro de ticket
                var voyageur = await _context.Voyageurs.FindAsync(numTicket);

                if (voyageur is null)
                    return StatusCode(404, "Réservation non trouvé.");

                // Supprime le voyageur (réservation) de la base de données
                _context.Remove(voyageur);

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la suppression a réussi
                if (result > 0)
                    return Ok("Réservation annulée avec succès !");

                return BadRequest("Échec de l'annulation de la réservation.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de l'annulation de la réservation : {e.Message}");
                throw;
            }
        }

        // Endpoint pour mettre à jour une réservation par le numéro de ticket
        [HttpPut("{numTicket}")]
        public async Task<IActionResult> EditVoyageur(int numTicket, Voyageur voyageur)
        {
            try
            {
                // Recherche le voyageur dans la base de données par son numéro de ticket
                var voyageurDansBD = await _context.Voyageurs.FindAsync(numTicket);

                if (voyageurDansBD is null)
                    return StatusCode(404, "Réservation non trouvé.");

                // Met à jour les propriétés du voyageur (réservation)
                voyageurDansBD.NumTicket = voyageur.NumTicket;
                voyageurDansBD.EmailVoyageur = voyageur.EmailVoyageur;
                voyageurDansBD.NomVoyageur = voyageur.NomVoyageur;
                voyageurDansBD.DateDepart = voyageur.DateDepart;
                voyageurDansBD.NbPlace = voyageur.NbPlace;

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la mise à jour a réussi
                if (result > 0)
                    return Ok("Réservation mise à jour avec succès !");

                return BadRequest("Échec de la mise à jour de la réservation.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de la mise à jour de la réservation : {e.Message}");
                throw;
            }
        }
    }
}
