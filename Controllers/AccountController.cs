using BancoApi.Data;
using BancoApi.Models;
using BancoApi.Services;
using BancoApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BancoApi.Controllers
{
    [ApiController]
    public class AccountControllers : ControllerBase
    {
        [HttpPost("account/login")]
        public IActionResult Login(
            [FromServices] AppDbContext context,
            [FromBody] AccountLoginViewModel model,
            [FromServices] TokenService tokenService)
        {
            try
            {
                var user = context.Users
                    .FirstOrDefault(x => x.Email == model.Email);

                if (user == null)
                    return StatusCode(401, new { message = "usuario ou senha invalido" });

                if (user.Password != Settings.GenerateHash(model.Password))
                    return StatusCode(401, new { message = "usuario ou senha invalido" });

                var token = tokenService.CreateToken(user);

                return Ok(new { token, user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno: " + ex.Message });
            }
        }

        //[Authorize(Roles = "adm, cliente")]
        [HttpPost("account/signup")]
        public IActionResult Signup(
            [FromBody] AccountSignupViewModel model,
            [FromServices] AppDbContext context)
        {
            try
            {
                var user = context.Users
                    .FirstOrDefault(x => x.Email == model.Email);

                if (user != null)
                    return StatusCode(401, new { message = "Email já cadastrado!" });

                var newUser = new User
                {
                    Email = model.Email,
                    Password = Settings.GenerateHash(model.Password),
                    Name = model.Name,
                    Role = "cliente",
                    Saldo = 0
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno: " + ex.Message });
            }
        }

        [Authorize(Roles = "adm")]
        [HttpPost("account/signup/adm")]
        public IActionResult SignupAdm(
            [FromBody] AccountSignupViewModel model,
            [FromServices] AppDbContext context)
        {
            try
            {
                var user = context.Users
                    .FirstOrDefault(x => x.Email == model.Email);

                if (user != null)
                    return StatusCode(401, new { message = "Email já cadastrado!" });

                var newUser = new User
                {
                    Email = model.Email,
                    Password = Settings.GenerateHash(model.Password),
                    Name = model.Name,
                    Role = "adm",
                    Saldo = 0
                };

                context.Users.Add(newUser);
                context.SaveChanges();
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno: " + ex.Message });
            }
        }

        [HttpGet("saldo")]
        [Authorize]
        public IActionResult SaldoByUser([FromServices] AppDbContext context)
        {
            try
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (string.IsNullOrWhiteSpace(userEmail))
                    return BadRequest(new { message = "Email do usuário não encontrado nos claims." });

                var user = context.Users.FirstOrDefault(x => x.Email == userEmail);

                if (user == null)
                    return NotFound(new { message = "Usuário inexistente!" });

                var saldo = user.Saldo;

                return Ok(new { saldo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno: " + ex.Message });
            }
        }
        [HttpPut("saldo/{email}")]
        [Authorize]
        public IActionResult Saldo(
            [FromBody] AccountSaldoUpdateViewModel viewModel,
            [FromServices] AppDbContext context,
            [FromRoute] string email)
        {
            try
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                    return NotFound(new { message = "Usuário inexistente!" });

                user.Saldo += viewModel.Saldo;

                user.Extrato = user.Extrato ?? new Extrato();
                user.Extrato.NumeroExtrato = viewModel.Saldo;
                user.Extrato.UserEmail = viewModel.Email;

                if (user.Saldo < 0)
                    return StatusCode(400, new { message = "O saldo não pode ser negativo!" });

                context.SaveChanges();
                return StatusCode(200, new { message = "O saldo foi alterado. Saldo atual: " + user.Saldo });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Erro ao atualizar o saldo: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno: " + ex.Message });
            }
        }

    }
}
