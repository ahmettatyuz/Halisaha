using System;
using Halisaha.Entities;

namespace Halisaha.DataAccess.Abstract
{
	public interface ITeamRepository
	{
		Task<Team> GetTeam(int id);

		Task<List<Team>> GetTeams();

		Task<Team> DeleteTeam(int id);

		Task<Team> UpdateTeam(Team team);

		Task<Team> CreateTeam(Team team);

		Task<List<Player>> GetPlayersInTeam(int teamId);
	}
}