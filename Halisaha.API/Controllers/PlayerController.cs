using Halisaha.Business.Abstract;
using Halisaha.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Halisaha.API.Security;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace Halisaha.API.Controllers
{
    using BCrypt.Net;
    using Halisaha.API.Models;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerController : Controller
    {
        private IPlayerService _playerService;
        private JwtAyarlari _jwtAyarlari;
        public PlayerController(IPlayerService playerService, IOptions<JwtAyarlari> jwtAyarlari)
        {
            _playerService = playerService;
            _jwtAyarlari = jwtAyarlari.Value;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Player player)
        {
            if (ModelState.IsValid)
            {
                var result = await _playerService.GetPlayerByPhone(player.Phone!);
                if (result == null)
                {
                    player.CreateDate = DateTime.Now;
                    await _playerService.CreatePlayer(player);
                    return Ok(player);
                }
                else
                {
                    return BadRequest("Bu telefon numarası başka kullanıcıya ait.");
                }
            }
            else
            {
                return BadRequest("Tüm zorunlu alanlar doldurulmalıdır.");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var result = await _playerService.GetPlayerByPhone(user.Phone);
            if (result == null) return NotFound("Kullanıcı bulunamadı");
            if (BCrypt.EnhancedVerify(user.Password, result.Password))
            {
                return Ok(CreateToken(result));
            }
            return NotFound("Telefon numarası veya parola hatalı.");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string CreateToken(Player player)
        {
            if (_jwtAyarlari.SecretKey == null) throw new Exception("Secret Key can not be null");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAyarlari.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("ID",player.Id.ToString()),
                new Claim("Firstname",player.FirstName!),
                new Claim("Lastname",player.Lastname!),
                new Claim("Role","player"),
            };

            var token = new JwtSecurityToken(_jwtAyarlari.Issuer, _jwtAyarlari.Audience, claims, expires: DateTime.Now.AddHours(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            return Ok(await _playerService.GetPlayers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            return Ok(await _playerService.GetPlayerById(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlayer([FromBody]Player player)
        {
            var result = await _playerService.GetPlayerById(player.Id);
            if (result != null)
            {
                player.Password = result.Password;
                return Ok(await _playerService.UpdatePlayer(player));
            }
            else
            {
                return NotFound("Oyuncu bulunamadı.");
            }
        }

        [HttpPut("password")]
        public async Task<IActionResult> PlayerResetPassword([FromBody] ChangePasswordModel model)
        {
            var result = await _playerService.GetPlayerById(model.id);
            bool verify = BCrypt.EnhancedVerify(model.oldPassword, result.Password);
            if (result != null)
            {
                result.Password= BCrypt.EnhancedHashPassword(model.password) ;
                return Ok(await _playerService.UpdatePlayer(result));
            }
            else
            {
                return NotFound("Oyuncu bulunamadı");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var result = await _playerService.GetPlayerById(id);
            if (result != null)
            {
                return Ok(_playerService.DeletePlayer(id));
            }
            else
            {
                return NotFound("Oyuncu bulunamadı.");
            }
        }

        [HttpPatch("joinTeam")]
        public async Task<IActionResult> PlayerJoinTeam(int playerId,int teamId)
        {
            return Ok(await _playerService.PlayerJoinTeam(playerId, teamId));
        }
    }
}

