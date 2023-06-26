namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Models;
    using WebApplication1.Services;

    namespace ControllerEquipe
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ControllerEquipe : ControllerBase
        {
            private readonly EquipeService _equipeservice;

            public ControllerEquipe(EquipeService equipeService) =>
                _equipeservice = equipeService;

            [HttpGet]
            public async Task<List<Equipes>> Get() =>
                await _equipeservice.GetAsync();

            [HttpGet("{id:length(24)}")]
            public async Task<ActionResult<Equipes>> Get(string id)
            {
                var equipe = await _equipeservice.GetAsync(id);

                if (equipe is null)
                {
                    return NotFound();
                }

                return equipe;
            }

            [HttpPost]
            public async Task<IActionResult> Post(Equipes newEquipe)
            {
                await _equipeservice.CreateAsync(newEquipe);

                return CreatedAtAction(nameof(Get), new { id = newEquipe.Id }, newEquipe);
            }

            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, Equipes updatedEquipe)
            {
                var equipe = await _equipeservice.GetAsync(id);

                if (equipe is null)
                {
                    return NotFound();
                }

                updatedEquipe.Id = equipe.Id;

                await _equipeservice.UpdateAsync(id, updatedEquipe);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var equipe = await _equipeservice.GetAsync(id);

                if (equipe is null)
                {
                    return NotFound();
                }

                await _equipeservice.RemoveAsync(id);

                return NoContent();
            }
        }
    }

}