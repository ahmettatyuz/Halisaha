using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Halisaha.API.Models;
using Halisaha.API.Security;
using Halisaha.Business.Abstract;
using Halisaha.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Halisaha.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    //[ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class OwnerController : Controller
    {
        private IOwnerService _ownerService;
        private JwtAyarlari _jwtAyarlari;
        public OwnerController(IOwnerService ownerService, IOptions<JwtAyarlari> jwtAyarlari)
        {
            _ownerService = ownerService;
            _jwtAyarlari = jwtAyarlari.Value;
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public string CreateToken(Owner owner)
        {
            if (_jwtAyarlari.SecretKey == null) throw new Exception("Secret Key can not be null");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAyarlari.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("ID",owner.Id.ToString()),
                new Claim("Firstname",owner.OwnerFirstName!),
                new Claim("Lastname",owner.OwnerLastName!),
            };

            var token = new JwtSecurityToken(_jwtAyarlari.Issuer, _jwtAyarlari.Audience, claims, expires: DateTime.Now.AddHours(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Owner owner)
        {
            if (ModelState.IsValid)
            {
                var result = await _ownerService.GetOwnerByPhone(owner.Phone!);
                if (result == null)
                {
                    return Ok(await _ownerService.CreateOwner(owner));
                }
                else
                {
                    return BadRequest("Bu telefon numarası başka bir kullanıcıya ait.");
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
            if (ModelState.IsValid)
            {
                var result = await _ownerService.GetOwnerByPhone(user.Phone);
                if (result == null) return Ok("Kullanıcı bulunamadı.");
                if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, result.Password))
                {
                    return Ok(CreateToken(result));
                }
                return Ok("Telefon numarası veya parola hatalı.");
            }
            else
            {
                return Ok("Tüm zorunlu alanlar doldurulmalıdır.");
            }
        }


        [HttpGet]
        public async Task<List<Owner>> GetOwners()
        {
            return await _ownerService.GetAllOwners();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOwner([FromBody] Owner owner)
        {
            var result = await _ownerService.GetOwnerById(owner.Id);
            if (result != null)
            {
                owner.Password = result.Password;
                return Ok(await _ownerService.UpdateOwner(owner));
            }
            else
            {
                return NotFound("İşyeri bulunamadı.");
            }
        }


        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword(int id, string password)
        {
            var result = await _ownerService.GetOwnerById(id);
            if (result != null)
            {
                result.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
                return Ok(await _ownerService.UpdateOwner(result));
            }
            else
            {
                return NotFound("İşyeri bulunamadı.");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var result = _ownerService.GetOwnerById(id);
            if (result != null)
            {
                return Ok(await _ownerService.DeleteOwner(id));
            }
            else
            {
                return NotFound("İşyeri bulunamadı.");
            }
        }

    }
}

