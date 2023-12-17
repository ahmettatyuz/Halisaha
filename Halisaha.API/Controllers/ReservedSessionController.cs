using Halisaha.Business.Abstract;
using Halisaha.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Halisaha.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReservedSessionController : Controller
    {
        private IReservedSessionService _reservedSessionService;
        public ReservedSessionController(IReservedSessionService reservedSessionService)
        {
            _reservedSessionService = reservedSessionService;
        }

        [HttpPost]
        public async Task<IActionResult> ReserveSession([FromBody]ReservedSession reservedSession)
        {
            reservedSession.CreateDate = DateTime.Now;
            return Ok(await _reservedSessionService.CreateReservedSession(reservedSession));
        }

        [HttpGet("PlayerReservedSessions")]
        public async Task<IActionResult> GetReservedSessionsByPlayerId(int playerId)
        {
            return Ok(await _reservedSessionService.GetReservedSessionsByPlayerId(playerId));
        }

        [HttpGet("OwnerReservedSessions")]
        public async Task<IActionResult> GetReservedSessionsByOwnerId(int ownerId)
        {
            return Ok(await _reservedSessionService.GetReservedSessionsByOwnerId(ownerId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservedSession([FromBody] ReservedSession reservedSession)
        {
            var result = await _reservedSessionService.GetReservedSessionById(reservedSession.Id);
            if (result != null)
            {
                return Ok(await _reservedSessionService.UpdateReservedSession(reservedSession));
            }
            else
            {
                return BadRequest("Rezerve edilmiş seans bulunamadı.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservedSession(int reservedSessionId)
        {
            var result = await _reservedSessionService.GetReservedSessionById(reservedSessionId);
            if(result != null)
            {
                return Ok(await _reservedSessionService.DeleteReservedSession(reservedSessionId));
            }
            else
            {
                return BadRequest("Rezerve edilmiş seans bulunamadı.");
            }
        }
    }
}

