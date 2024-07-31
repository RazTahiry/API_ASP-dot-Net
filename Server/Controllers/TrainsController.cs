using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // Contrôleur pour gérer les opérations sur les trains
    public class TrainsController(AppDbContext context) : ControllerBase
    {
        // Initialisation du contexte de la base de données
        private readonly AppDbContext _context = context;

        // Endpoint pour obtenir tous les trains
        [HttpGet]
        public async Task<IEnumerable<Train>> GetTrains()
        {
            // Récupère tous les trains sans suivi (pour optimiser les performances)
            var trains = await _context.Trains.AsNoTracking().ToListAsync();

            return trains;
        }

        // Endpoint pour créer un nouveau train
        [HttpPost]
        public async Task<IActionResult> Create(Train train)
        {
            try
            {
                // Vérifie si le modèle est valide
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Ajoute le nouveau train à la base de données
                await _context.Trains.AddAsync(train);

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la sauvegarde a réussi
                if (result > 0)
                    return Ok("Train ajouté avec succès !");

                return BadRequest("On n'a pas pu ajouter le train.");                
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de l'ajout du train : {e.Message}");
                throw;
            }
        }

        // Endpoint pour obtenir un train spécifique par son immatriculation
        [HttpGet("{immatriculation}")]
        public async Task<ActionResult<Train>> GetTrain(string immatriculation)
        {
            // Recherche le train dans la base de données par son immatriculation
            var train = await _context.Trains.FindAsync(immatriculation);

            if (train is null)
                return NotFound("Train non trouvé !");

            return Ok(train);
        }

        // Endpoint pour supprimer un train par son immatriculation
        [HttpDelete("{immatriculation}")]
        public async Task<IActionResult> Delete(string immatriculation)
        {
            try
            {
                // Recherche le train dans la base de données par son immatriculation
                var train = await _context.Trains.FindAsync(immatriculation);

                if (train is null)
                    return NotFound("Train non trouvé.");
                
                // Supprime le train de la base de données
                _context.Trains.Remove(train);

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la suppression a réussi
                if (result > 0)
                    return Ok("Train supprimé avec succès !");

                return BadRequest("Échec de la suppression du train.");                
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de la suppression du train : {e.Message}");
                throw;
            }
        }

        // Endpoint pour mettre à jour un train par son immatriculation
        [HttpPut("{immatriculation}")]
        public async Task<IActionResult> EditTrain(string immatriculation, Train train)
        {
            try
            {
                // Recherche le train dans la base de données par son immatriculation
                var trainDansBD = await _context.Trains.FindAsync(immatriculation);

                if (trainDansBD is null)
                    return NotFound("Train non trouvé.");

                // Vérifie si l'immatriculation du train peut être modifiée
                if (trainDansBD.Immatriculation != train.Immatriculation)
                    return BadRequest("L'immatriculation du train ne peut pas être modifiée.");

                // Met à jour les propriétés du train
                trainDansBD.CodeItineraire = train.CodeItineraire;

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la mise à jour a réussi
                if (result > 0)
                    return Ok("Train mis à jour avec succès !");

                return BadRequest("Échec de la mise à jour du train.");                
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de la mise à jour du train : {e.Message}");
                throw;
            }
        }
    }
}
