using System;
using Halisaha.Business.Abstract;
using Halisaha.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Halisaha.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        private ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            List<Team> teams = await _teamService.GetTeams();
            foreach (Team team in teams)
            {
                team.Players = await _teamService.GetPlayersInTeam(team.Id);
            }
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var result = await _teamService.GetTeam(id);

            if (result != null)
            {
                result.Players = await _teamService.GetPlayersInTeam(result.Id);
                return Ok(result);
            }
            else
            {
                return NotFound("Takım bulunamadı.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeam(int id, int playerId)
        {
            var result = await _teamService.GetTeam(id);
            if (result == null)
            {
                return BadRequest("Takım bulunamadı.");
            }
            else
            {
                await _teamService.DeleteTeam(id, playerId);
            }
            return Ok("Takım silindi");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            return Ok(await _teamService.CreateTeam(team));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeam(int id,string name)
        {

            var result = await _teamService.GetTeam(id);
            result.Name = name;
            if (result != null)
            {
                return Ok(await _teamService.UpdateTeam(result));
            }
            else
            {
                return NotFound("Takım bulunamadı.");
            }

        }

        [HttpGet("playersTeam")]
        public async Task<IActionResult> GetPlayersTeam(int playerId)
        {
            return Ok(await _teamService.GetPlayersTeam(playerId));
        }

        [HttpGet("teamIncludePlayer")]
        public async Task<IActionResult> TeamsIncludedPlayer(int playerId){
            return Ok(await _teamService.GetTeamIncludedPlayer(playerId));
        }

        [HttpDelete("player")]
        public async Task<IActionResult> DeletePlayerFromTeam(int playerId, int teamId)
        {
            bool result = await _teamService.DeletePlayerFromTeam(playerId, teamId);
            if (result)
            {
                return Ok("Oyuncu takımdan silindi");
            }
            else
            {
                return NotFound("Oyuncu silinemedi");
            }
        }

    }
}

