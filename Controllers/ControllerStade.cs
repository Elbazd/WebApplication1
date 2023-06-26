namespace WebApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Models;
    using WebApplication1.Services;

    namespace ControllerStade
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ControllerStade : ControllerBase
        {
            private readonly StadeService _stadeservice;

            public ControllerStade(StadeService stadeService) =>
                _stadeservice = stadeService;

            [HttpGet]
            public async Task<List<Stades>> Get()
            {
                return await _stadeservice.GetAsync();
            }

            [HttpGet("{id:length(24)}")]
            public async Task<ActionResult<Stades>> Get(string id)
            {
                var stade = await _stadeservice.GetAsync(id);

                if (stade is null)
                {
                    return NotFound();
                }

                return stade;
            }

            [HttpPost]
            public async Task<IActionResult> Post(Stades newStade)
            {
                await _stadeservice.CreateAsync(newStade);

                return CreatedAtAction(nameof(Get), new { id = newStade.Id });
            }

            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, Stades updatedStade)
            {
                var stade = await _stadeservice.GetAsync(id);

                if (stade is null)
                {
                    return NotFound();
                }

                updatedStade.Id = stade.Id;

                await _stadeservice.UpdateAsync(id, updatedStade);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var stade = await _stadeservice.GetAsync(id);

                if (stade is null)
                {
                    return NotFound();
                }

                await _stadeservice.RemoveAsync(id);

                return NoContent();
            }
        }
    }

}