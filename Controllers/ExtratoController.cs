// //using BancoApi.Attributes;
// using BancoApi.Data;
// using BancoApi.Models;
// using BancoApi.ViewModels;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace BancoApi.Controllers
// {
//     [ApiController]
//     public class ExtratoController : ControllerBase
//     {
//         [Authorize(Roles = "adm, cliente")]
//         [HttpPost("extrato")]
//         public IActionResult Post(
//             [FromServices] AppDbContext context,
//             [FromBody] Extrato model
//             )
//         {
//             try
//             {
//                 var user = context.Contas
//                     .FirstOrDefault(x => x.Id == model.Id);

//                 if (user == null)
//                     return NotFound(new { message = "Conta não existente!" });

//                 var newExtrato = new Extrato
//                 {
//                     Id = model.Id,
//                     ExtratoId = model.ExtratoId,
//                     User = user
//                 };

//                 context.Extratos.Add(newExtrato);
//                 context.SaveChanges();

//                 return Ok();
//             }
//             catch
//             {
//                 return StatusCode(500, new { message = "Erro interno" });
//             }
//         }

//         [HttpPut("product")]
//         public IActionResult Put()
//         {
//             return Ok();
//         }

//         [HttpDelete("product")]
//         public IActionResult Delete()
//         {
//             return Ok();
//         }

//         [Authorize(Roles = "adm, cliente")]
//         [HttpGet("product")]
//         public IActionResult Get(
//             [FromServices] AppDbContext context,
//             [FromQuery] int page = 0,
//             [FromQuery] int pageSize = 10)
//         {
//             try
//             {
//                 var count = context.Products.AsNoTracking().Count();

//                 var products = context.Products
//                     .Include(x => x.Category)
//                     .Skip(page * pageSize)
//                     .Take(pageSize)
//                     .AsNoTracking()
//                     .ToList();
                                
//                 return Ok(new
//                 {       
//                    count,
//                    page,
//                    pageSize,
//                    products
//                 });
//             }
//             catch
//             {
//                 return StatusCode(500, new { message = "Erro interno" });
//             }
//         }

//         [ApiKey]
//         [HttpGet("product/revendedor")]
//         public IActionResult GetRevendedor(
//             [FromServices] AppDbContext context)
//         {
//             try
//             {
//                 var products = context.Products
//                     .Include(x => x.Category)
//                     .Select(x => new { 
//                         x.Name,
//                         x.Description,
//                         x.Image,
//                         value = ((double)x.Value * 0.85),
//                         x.Category
//                     })
//                     .ToList();

//                 return Ok(products);
//             }
//             catch
//             {
//                 return StatusCode(500, new { message = "Erro interno" });
//             }
//         }
//     }
// }
