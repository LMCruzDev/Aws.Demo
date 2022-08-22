using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = new string[] { "Oxtail", "Curry Chicken", "Dumplings" };

            if (!recipes.Any())
                return NotFound();

            return Ok(recipes);
        }
    }
}
