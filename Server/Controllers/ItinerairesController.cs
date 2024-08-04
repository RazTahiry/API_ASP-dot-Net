using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin")]
    // Contrôleur pour gérer les opérations sur les itinéraires
    public class itinerairesController(AppDbContext context) : ControllerBase
    {
        // Initialisation du contexte de la base de données
        private readonly AppDbContext _context = context;

        // Endpoint pour obtenir tous les itinéraires
        [HttpGet]
        public async Task<IEnumerable<Itineraire>> GetItineraires()
        {
            // Récupère tous les itinéraires sans suivi (pour optimiser les performances)
            var itineraires = await _context.Itineraires.AsNoTracking().ToListAsync();

            return itineraires;
        }

        // Endpoint pour créer un nouvel itinéraire
        [HttpPost]
        public async Task<IActionResult> Create(Itineraire itineraire)
        {
            try
            {
                // Vérifie si le modèle est valide
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Ajoute le nouvel itinéraire à la base de données
                await _context.Itineraires.AddAsync(itineraire);

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la sauvegarde a réussi
                if (result > 0)
                    return Ok("Itinéraire ajouté avec succès!");

                return BadRequest("On n'a pas pu ajouter l'itinéraire.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de l'ajout de l'itinéraire : {e.Message}");
                throw;
            }
        }

        // Endpoint pour obtenir un itinéraire spécifique par son code
        [HttpGet("{CodeItineraire}")]
        public async Task<ActionResult<Itineraire>> GetItineraire(string CodeItineraire)
        {
            // Recherche l'itinéraire dans la base de données par son code
            var itineraire = await _context.Itineraires.FindAsync(CodeItineraire);

            if (itineraire is null)
                return NotFound("Itinéraire non trouvé.");

            return Ok(itineraire);
        }

        // Endpoint pour supprimer un itinéraire par son code
        [HttpDelete("{CodeItineraire}")]
        public async Task<IActionResult> Delete(string CodeItineraire)
        {
            try
            {
                // Recherche l'itinéraire dans la base de données par son code
                var itineraire = await _context.Itineraires.FindAsync(CodeItineraire);

                if (itineraire is null)
                    return NotFound("Itinéraire non trouvé.");

                // Supprime l'itinéraire de la base de données
                _context.Itineraires.Remove(itineraire);

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la suppression a réussi
                if (result > 0)
                    return Ok("Itinéraire supprimé avec succès !");

                return BadRequest("Échec de la suppression de l'itinéraire.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de la suppression de l'itinéraire : {e.Message}");
                throw;
            }
        }

        // Endpoint pour mettre à jour un itinéraire par son code
        [HttpPut("{CodeItineraire}")]
        public async Task<IActionResult> EditItineraire(string CodeItineraire, Itineraire itineraire)
        {
            try
            {
                // Recherche l'itinéraire dans la base de données par son code
                var itineraireDansBD = await _context.Itineraires.FindAsync(CodeItineraire);

                if (itineraireDansBD is null)
                    return NotFound("Itinéraire non trouvé.");

                // Vérifie si le code de l'itinéraire peut être modifié
                if (itineraireDansBD.CodeItineraire != itineraire.CodeItineraire)
                    return BadRequest("Le code itinéraire ne peut pas être modifié.");

                // Met à jour les propriétés de l'itinéraire
                itineraireDansBD.LieuDepart = itineraire.LieuDepart;
                itineraireDansBD.LieuArrivee = itineraire.LieuArrivee;
                itineraireDansBD.HeureDepart = itineraire.HeureDepart;
                itineraireDansBD.JourDepart = itineraire.JourDepart;

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la mise à jour a réussi
                if (result > 0)
                    return Ok("Itinéraire mis à jour avec succès !");

                return BadRequest("Échec de la mise à jour de l'itinéraire.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de la mise à jour de l'itinéraire : {e.Message}");
                throw;
            }
        }
    }
}
