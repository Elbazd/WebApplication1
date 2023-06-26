namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Models;
    using WebApplication1.Services;

    namespace ControllerJoueur
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ControllerJoueur : ControllerBase
        {
            private readonly JoueurService _joueurservice;

            public ControllerJoueur(JoueurService joueurService) =>
                _joueurservice = joueurService;

            [HttpGet]
            public async Task<List<Joueurs>> Get() => await _joueurservice.GetAsync();

            [HttpGet("{id:length(24)}")]
            public async Task<ActionResult<Joueurs>> Get(string id)
            {
                var joueur = await _joueurservice.GetAsync(id);

                if (joueur is null)
                {
                    return NotFound();
                }

                return joueur;
            }

            [HttpPost]
            public async Task<IActionResult> Post(Joueurs newJoueur)
            {
                await _joueurservice.CreateAsync(newJoueur);

                return CreatedAtAction(nameof(Get), new { id = newJoueur.Id });
            }

            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, Joueurs updatedJoueur)
            {
                var joueur = await _joueurservice.GetAsync(id);

                if (joueur is null)
                {
                    return NotFound();
                }

                updatedJoueur.Id = joueur.Id;

                await _joueurservice.UpdateAsync(id, updatedJoueur);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var joueur = await _joueurservice.GetAsync(id);

                if (joueur is null)
                {
                    return NotFound();
                }

                await _joueurservice.RemoveAsync(id);

                return NoContent();
            }
        }
    }

}