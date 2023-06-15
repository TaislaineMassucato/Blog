using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Blog.Controllers
{
    [ApiController]

    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]

        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {          
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05xe12 - Falha interna no Servidor"));
            }
        }

        [HttpGet("v1/categories/{id:int}")]

        public async Task<IActionResult> GetAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (categories == null)
                {
                    return NotFound(new ResultViewModel<Category>("Conteudo não encontrado"));
                }
                else

                    return Ok(new ResultViewModel<Category>(categories));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe16 - Falha interna no Servidor"));
            }
        }

        [HttpPost("v1/categories")]

        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
            
            try
            {
                var category = new Category 
                {
                    Name = model.Name,
                    Slug= model.Slug.ToLower()
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe9 - Não foi possível incluir a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe9 - Falha interna no Servidor"));
            }
        }

        [HttpPut("v1/categories/{id:int}")]

        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (categories == null)
                {
                    return NotFound(new ResultViewModel<Category>("Id Não encontrado"));
                }
                categories.Name = model.Name;
                categories.Slug = model.Slug;

                context.Categories.Update(categories);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(categories));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe11 - Não foi possível alterar a categoria"));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe12 - Falha interna no Servidor"));
            }
        }

        [HttpDelete("v1/categories/{id:int}")]

        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (categories == null)
                {
                    return NotFound(new ResultViewModel<Category>("Id Não encontrado"));
                }

                context.Categories.Remove(categories);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(categories));

            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05xe13 - Não foi possível remover a categoria");
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe14 - Falha interna no Servidor"));
            }
        }
    }
}
