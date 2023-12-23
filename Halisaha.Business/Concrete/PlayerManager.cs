using System;
using Halisaha.Business.Abstract;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;
namespace Halisaha.Business.Concrete
{
    using System.Collections.Generic;
    using BCrypt.Net;
    public class PlayerManager : IPlayerService
    {
        private IPlayerRepository _playerRepository;
        public PlayerManager(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async  Task<Player> CreatePlayer(Player player)
        {
            player.Password = BCrypt.EnhancedHashPassword(player.Password);
            return await _playerRepository.CreatePlayer(player);
        }

        public async Task<Player> DeletePlayer(int id)
        {
            return await _playerRepository.DeletePlayer(id);
        }

        public async Task<Player> GetPlayerById(int id)
        {
            return await _playerRepository.GetPlayerById(id);
        }

        public async Task<Player> GetPlayerByPhone(string phone)
        {
            return await _playerRepository.GetPlayerByPhone(phone);
        }

        public async Task<List<Player>> GetPlayers()
        {
            return await _playerRepository.GetPlayers();
        }

        public async Task<Player> PlayerJoinTeam(int playerId, int teamId)
        {
            return await _playerRepository.PlayerJoinTeam(playerId, teamId);
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            
            return await _playerRepository.UpdatePlayer(player);
        }
    }
}

