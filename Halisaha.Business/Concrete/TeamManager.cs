using System;
using Halisaha.Business.Abstract;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;

namespace Halisaha.Business.Concrete
{
    public class TeamManager : ITeamService
    {
        private ITeamRepository _teamRepository;
        public TeamManager(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Team> CreateTeam(Team team)
        {
            return await _teamRepository.CreateTeam(team);
        }

        public async Task<Team> DeleteTeam(int id)
        {
            return await _teamRepository.DeleteTeam(id);
        }

        public async Task<List<Player>> GetPlayersInTeam(int teamId)
        {
            return await _teamRepository.GetPlayersInTeam(teamId);
        }

        public async Task<Team> GetTeam(int id)
        {
            return await _teamRepository.GetTeam(id);
        }

        public async Task<List<Team>> GetTeams()
        {
            return await _teamRepository.GetTeams();
        }

        public async Task<Team> UpdateTeam(Team team)
        {
            return await _teamRepository.UpdateTeam(team);
        }
    }
}

