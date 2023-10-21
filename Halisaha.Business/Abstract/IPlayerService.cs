﻿using System;
using Halisaha.Entities;

namespace Halisaha.Business.Abstract
{
	public interface IPlayerService
	{
        Task<List<Player>> GetPlayers();
        Task<Player> GetPlayerById(int id);

        Task<Player> GetPlayerByPhone(string phone);

        Task<Player> DeletePlayer(int id);

        Task<Player> CreatePlayer(Player player);

        Task<Player> UpdatePlayer(Player player);
    }
}
