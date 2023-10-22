using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halisaha.Business.Abstract;
using Halisaha.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Halisaha.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class SessionController : Controller
    {
        private ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(Session session)
        {
            if (session.OwnerId > 0)
            {
                return Ok(await _sessionService.CreateSession(session));
            }
            else
            {
                return BadRequest("OwnerId 0'dan büyük olmalıdır.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSessionsByOwnerId(int ownerId)
        {
            if (ownerId > 0)
            {
                return Ok(await _sessionService.GetSessionsByOwnerId(ownerId));
            }
            else
            {
                return BadRequest("OwnerId 0'dan büyük olmalıdır.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSession([FromBody]Session session)
        {
            var result = _sessionService.GetSessionById(session.Id);
            if (result != null)
            {
                return Ok(await _sessionService.UpdateSession(session));
            }
            else
            {
                return NotFound("Seans bulunamadı.");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var result = _sessionService.GetSessionById(id);
            if (result != null)
            {
                return Ok(await _sessionService.DeleteSession(id));
            }
            else
            {
                return NotFound("Seans bulunamadı.");
            }
        }
    }
}

