//using BancoApi.Attributes;
using BancoApi.Data;
using BancoApi.Models;
using BancoApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BancoApi.Controllers
{
    [ApiController]
    public class ExtratoController : ControllerBase
    {
        [HttpGet("extrato")]
        public IActionResult GetExtrato(
            [FromServices] AppDbContext context
        )
        {
            try
            {
                var Authorization = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (string.IsNullOrWhiteSpace(Authorization))
                    return BadRequest(new { message = "Usuario não autenticado para acessar essa rota." });

                var user = context.Users.FirstOrDefault(x => x.Email == Authorization);

                if (user == null)
                    return NotFound(new { message = "Usuário inexistente!" });


                var extratos = context.Extratos.Where(e => e.User == Authorization).ToList();

                return Ok(extratos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno" + ex.Message });
            }
        }

    }
}
