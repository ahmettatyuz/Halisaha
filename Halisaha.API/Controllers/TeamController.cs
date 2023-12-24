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
            foreach(Team team in teams){
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
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var result = await _teamService.GetTeam(id);
            if (result == null)
            {
                return BadRequest("Takım bulunamadı.");
            }
            return Ok(await _teamService.DeleteTeam(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            return Ok(await _teamService.CreateTeam(team));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeam(Team team)
        {
            var result = await _teamService.GetTeam(team.Id);

            if (result != null)
            {
                return Ok(await _teamService.UpdateTeam(team));
            }
            else
            {
                return NotFound("Takım bulunamadı.");
            }

        }

    }
}

