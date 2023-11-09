using BancoApi.Data;
using BancoApi.Models;
using BancoApi.Services;
using BancoApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


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
                    return StatusCode(401, new {message = "usuario ou senha invalido"});

                if(user.Password != Settings.GenerateHash(model.Password))
                    return StatusCode(401, new { message = "usuario ou senha invalido" });
                
                var token = tokenService.CreateToken(user);

                return Ok(new { token });
            }
            catch
            {
                return StatusCode(500, new { message = "Erro interno" });
            }
        }

        [Authorize(Roles = "adm, cliente")]
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
                    Saldo = 0,
                    Role = "cliente"
                };
                    
                context.Users.Add(newUser);
                context.SaveChanges();                

                return Ok();
            }
            catch
            {
                return StatusCode(500, new { message = "Erro interno" });
            }
        }


        [HttpGet("saldo/{email:string}")]
        public IActionResult Saldo(
            [FromBody] AccountSaldoViewModel model,
            [FromServices] AppDbContext context,
            [FromRoute] string email
            )
        {
            try
            {
                var user = context.Users.Find(email);
                if (user == null)
                    return NotFound(new { message = "Usuario inexistente!" });

                var saldo = context.Users.Find(user.Saldo);         

                return Ok(saldo);
            }
            catch
            {
                return StatusCode(500, new { message = "Erro interno" });
            }
        }

        [HttpPut("saldo/{email:string}")]
        public IActionResult ChangeSaldo(
            [FromBody] AccountSaldoUpdateViewModel viewModel,
            [FromServices] AppDbContext context,
            [FromRoute] string email
            )
        {
            try
            {
                var user = context.Users.Find(email);
                if (user == null)
                    return NotFound(new { message = "Usuario inexistente!" });

                var saldo = context.Users.Find(user.Saldo);
                user.Saldo += viewModel.Saldo; 
                
                user.Extrato.NumeroExtrato = viewModel.Saldo;
                user.Extrato.UserEmail = viewModel.Email;

                if(user.Saldo < 0)
                    return StatusCode(400, new { message = "O saldo não pode ser negativo!" });
                
                context.SaveChanges();
                return StatusCode(200, new { message = "O saldo foi alterado. Saldo atual: " + user.Saldo });;
            }
            catch
            {
                return StatusCode(500, new { message = "Erro interno" });
            }
        }
    }
}
