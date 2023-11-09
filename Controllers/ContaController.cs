// using BancoApi.Data;
// using BancoApi.Models;
// using BancoApi.ViewModels;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Caching.Memory;

// namespace BancoApi.Controllers
// {
//     [Authorize(Roles = "adm")]
//     [ApiController]
//     public class ContaController : ControllerBase
//     {        
//         [HttpPost("conta")]
//         public async Task<IActionResult> PostAsync(
//             [FromServices] AppDbContext context,
//             [FromBody] ContaCreateViewModel viewModel)
//         {
//             try
//             {   
                
                

//                 var newExtrato = new Conta {
//                     Saldo = 0,


//                 };

//                 await context.Categories.AddAsync(newCategory);
//                 await context.SaveChangesAsync();

//                 return Ok();
//             }
//             catch
//             {
//                 return StatusCode(500, new { message = "Erro interno" });
//             }
//         }

//         [HttpPut("conta/deposito")]
//         public async Task<IActionResult> PutAsync(
//             [FromServices] AppDbContext context,
//             [FromBody] CategoryUpdateViewModel viewModel,
//             [FromRoute] int id)
//         {
//             try
//             {
//                 var category = await context.Categories.FindAsync(id);

//                 if(category == null)
//                     return NotFound();

//                 category.Name = viewModel.Name;

//                 await context.SaveChangesAsync();

//                 return Ok();
//             }
//             catch
//             {
//                 return StatusCode(500, new { message = "Erro interno" });
//             }
//         }

//         [HttpPut("conta/saque")]
//         public async Task<IActionResult> PutAsync(
//             [FromServices] AppDbContext context,
//             [FromBody] CategoryUpdateViewModel viewModel,
//             [FromRoute] int id)
//         {
//             try
//             {
//                 var category = await context.Categories.FindAsync(id);

//                 if(category == null)
//                     return NotFound();

//                 category.Name = viewModel.Name;

//                 await context.SaveChangesAsync();

//                 return Ok();
//             }
//             catch
//             {
//                 return StatusCode(500, new { message = "Erro interno" });
//             }
//         }

//         [HttpDelete("conta/{id:int}")]
//         public async Task<IActionResult> DeleteAsync(
//             [FromServices] AppDbContext context,            
//             [FromRoute] int id)
//         {
//             try
//             {
//                 var category = await context.Categories.FindAsync(id);

//                 if (category == null)
//                     return NotFound();
                                
//                 context.Categories.Remove(category);
//                 await context.SaveChangesAsync();

//                 return Ok();
//             }
//             catch
//             {
//                 return StatusCode(500, new { message = "Erro interno" });
//             }
//         }

//         [HttpGet("category")]
//         public async Task<IActionResult> GetAsync(
//             [FromServices] AppDbContext context,
//             [FromServices] IMemoryCache cache)
//         {
//             try
//             {
//                 //COM CACHE
//                 var categories = await cache
//                     .GetOrCreateAsync("CategoriesCache", async entry =>
//                     {
//                         entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
//                         return await context.Categories.ToListAsync();
//                     });

//                 //SEM CACHE
//                 //var categories = await context
//                 //    .Categories
//                 //    .ToListAsync();

//                 return Ok(categories);
//             }
//             catch
//             {
//                 return StatusCode(500, new { message = "Erro interno" });
//             }
//         }
//     }
// }
