//using BancoApi.Attributes;
using BancoApi.Data;
using BancoApi.Models;
using BancoApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoApi.Controllers
{
    [ApiController]
    public class ExtratoController : ControllerBase
    {

        [HttpGet("extrato/{email}")]
        public IActionResult GetExtrato(
            [FromServices] AppDbContext context,
            [FromRoute] string email
        )
        {
            try
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                    return NotFound(new { message = "Conta não existente!" });

                var extratos = context.Extratos.Where(e => e.UserEmail == email).ToList();

                return Ok(extratos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno" + ex.Message });
            }
        }

    }
}
