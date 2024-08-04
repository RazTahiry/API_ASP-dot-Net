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
    // Contrôleur pour gérer les opérations sur les catégories
    public class categoriesController(AppDbContext context) : ControllerBase
    {
        // Initialisation du contexte de la base de données
        private readonly AppDbContext _context = context;

        // Endpoint pour obtenir toutes les catégories
        [HttpGet]
        public async Task<IEnumerable<Categorie>> GetCategorie()
        {
            // Récupère toutes les catégories sans suivi (pour optimiser les performances)
            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            return categories;
        }

        // Endpoint pour créer une nouvelle catégorie
        [HttpPost]
        public async Task<IActionResult> Create(Categorie categorie)
        {
            try
            {
                // Vérifie si le modèle est valide
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Ajoute la nouvelle catégorie à la base de données
                await _context.Categories.AddAsync(categorie);

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la sauvegarde a réussi
                if (result > 0)
                    return Ok("Catégorie ajoutée avec succès !");

                return BadRequest("On n'a pas pu ajouter la catégorie.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de l'ajout de la catégorie : {e.Message}");
                throw;
            }
        }

        // Endpoint pour obtenir une catégorie spécifique par son code
        [HttpGet("{CodeCategorie}")]
        public async Task<ActionResult<Categorie>> GetCategorie(string CodeCategorie)
        {
            // Recherche la catégorie dans la base de données par son code
            var categorie = await _context.Categories.FindAsync(CodeCategorie);

            if (categorie is null)
                return NotFound("Catégorie non trouvée.");

            return Ok(categorie);
        }

        // Endpoint pour supprimer une catégorie par son code
        [HttpDelete("{CodeCategorie}")]
        public async Task<IActionResult> Delete(string CodeCategorie)
        {
            try
            {
                // Recherche la catégorie dans la base de données par son code
                var categorie = await _context.Categories.FindAsync(CodeCategorie);

                if (categorie is null)
                    return NotFound("Catégorie non trouvée.");

                // Supprime la catégorie de la base de données
                _context.Categories.Remove(categorie);

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la suppression a réussi
                if (result > 0)
                    return Ok("Catégorie supprimée avec succès !");

                return BadRequest("Échec de la suppression de la catégorie.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de la suppression de la catégorie : {e.Message}");
                throw;
            }
        }

        // Endpoint pour mettre à jour une catégorie par son code
        [HttpPut("{CodeCategorie}")]
        public async Task<IActionResult> EditCategorie(string CodeCategorie, Categorie categorie)
        {
            try
            {
                // Recherche la catégorie dans la base de données par son code
                var categorieDansBD = await _context.Categories.FindAsync(CodeCategorie);

                if (categorieDansBD is null)
                    return NotFound("Catégorie non trouvée.");

                // Vérifie si le code de la catégorie peut être modifié
                if (categorieDansBD.CodeCategorie != categorie.CodeCategorie)
                    return BadRequest("Le code catégorie ne peut pas être modifié.");

                // Met à jour les propriétés de la catégorie
                categorieDansBD.LibelleCategorie = categorie.LibelleCategorie;
                categorieDansBD.NbPlaceSupporte = categorie.NbPlaceSupporte;
                categorieDansBD.Frais = categorie.Frais;

                // Sauvegarde les changements dans la base de données
                var result = await _context.SaveChangesAsync();

                // Vérifie si la mise à jour a réussi
                if (result > 0)
                    return Ok("Catégorie mise à jour avec succès !");

                return BadRequest("Échec de la mise à jour de la catégorie.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erreur lors de la mise à jour de la catégorie : {e.Message}");
                throw;
            }
        }
    }
}
