using System;
using Halisaha.Entities;

namespace Halisaha.DataAccess.Abstract
{
	public interface IPlayerRepository
	{
        Task<List<Player>> GetPlayers();

		Task<Player> GetPlayerById(int id);

        Task<Player> GetPlayerByPhone(string phone);

        Task<Player> DeletePlayer(int id);

		Task<Player> CreatePlayer(Player player);

		Task<Player> UpdatePlayer(Player player);

		Task<Player> PlayerJoinTeam(int playerId,int teamId);

		Task<List<Team>> GetTeamsForPlayer(int playerId);

	}
}

