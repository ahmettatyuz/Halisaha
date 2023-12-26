using System;
using Halisaha.Entities;

namespace Halisaha.Business.Abstract
{
    public interface ITeamService
    {
        Task<Team> GetTeam(int id);

        Task<List<Team>> GetTeams();

        Task<Team> DeleteTeam(int id,int playerId);

        Task<Team> UpdateTeam(Team team);

        Task<Team> CreateTeam(Team team);

        Task<List<Player>> GetPlayersInTeam(int teamId);

        Task<List<Team>> GetPlayersTeam(int playerId);

        Task<bool> DeletePlayerFromTeam(int playerId, int teamId);

        Task<List<Team>> GetTeamIncludedPlayer(int playerId);
    }
}

