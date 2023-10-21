using System;
using System.Numerics;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Halisaha.DataAccess.Concrete
{
    public class PlayerRepository : IPlayerRepository
    {
        public async Task<Player> CreatePlayer(Player player)
        {
            using(var halisahaDbContext = new HalisahaDbContext())
            {
                halisahaDbContext.Players.Add(player);
                await halisahaDbContext.SaveChangesAsync();
                return player;
            }
        }

        public async Task<Player> DeletePlayer(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                var deletedPlayer = await halisahaDbContext.Players.FindAsync(id);
                halisahaDbContext.Players.Remove(deletedPlayer);
                await halisahaDbContext.SaveChangesAsync();
                return deletedPlayer;
            }
        }

        public async Task<Player> GetPlayerById(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Players.FindAsync(id);
            }
        }

        public async Task<Player> GetPlayerByPhone(string phone)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return halisahaDbContext.Players.FirstOrDefault(x => x.Phone == phone);
            }
        }

        public async Task<List<Player>> GetPlayers()
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Players.ToListAsync();
            }
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                halisahaDbContext.Players.Update(player);
                await halisahaDbContext.SaveChangesAsync();
                return player;
            }
        }
    }
}