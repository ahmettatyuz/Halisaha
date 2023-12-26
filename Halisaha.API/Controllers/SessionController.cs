using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Halisaha.Business.Abstract;
using Halisaha.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Halisaha.API.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    //[ApiController]
    public class SessionController : Controller
    {
        private ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
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

        [HttpGet("owner")]
        public async Task<IActionResult> GetSessionsByOwnerId(int ownerId)
        {
            if (ownerId > 0)
            {
                List<Session> sessions = await _sessionService.GetSessionsByOwnerId(ownerId);
                List<Session> sortedSessions = sessions.OrderBy(session => DateTime.ParseExact(session.SessionTime, "H:m", CultureInfo.InvariantCulture)).ToList();
                foreach (Session session in sortedSessions)
                {
                    session.SessionTime = DateTime.ParseExact(session.SessionTime, "H:m", CultureInfo.InvariantCulture).TimeOfDay.ToString().Substring(0, 5);
                }
                return Ok(sortedSessions);
            }
            else
            {
                return BadRequest("OwnerId 0'dan büyük olmalıdır.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSession([FromBody] Session session)
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

        [HttpGet("id")]
        public async Task<IActionResult> GetSessionById(int sessionId)
        {
            var result = await _sessionService.GetSessionById(sessionId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Seans Bulunamadı");
            }
        }
    }
}