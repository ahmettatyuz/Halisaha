using System;
using System.Diagnostics;
using System.Numerics;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Halisaha.DataAccess.Concrete
{
    public class PlayerRepository : IPlayerRepository
    {
        public async Task<Player> CreatePlayer(Player player)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
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
                List<Team> teams = await GetTeamsForPlayer(id);
                var playerandteams = await halisahaDbContext.Players.Where(x => x.Id == id).FirstOrDefaultAsync();
                playerandteams.Teams = teams;
                return playerandteams;
                // return await halisahaDbContext.Players.Include(x => x.Teams).Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<Player> GetPlayerByPhone(string phone)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Players.FirstOrDefaultAsync(x => x.Phone == phone);
            }
        }

        public async Task<List<Player>> GetPlayers()
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {

                List<Player> players = await halisahaDbContext.Players.ToListAsync();
                foreach (Player player in players)
                {
                    player.Teams = await GetTeamsForPlayer(player.Id);
                }
                return players;
            }
        }

        public async Task<List<Team>> GetTeamsForPlayer(int playerId)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                List<Team> teams = new List<Team>();
                List<PlayerTeam> playerTeams = await halisahaDbContext.PlayerTeams.Where(x => x.PlayerId == playerId && x.Deleted == 0).Include(x => x.Team).ToListAsync();
                foreach (PlayerTeam playerTeam in playerTeams)
                {
                    teams.Add(playerTeam.Team);
                }
                return teams.Where(x => x.Deleted == 0).ToList();
            }

        }

        public async Task<Player> PlayerJoinTeam(int playerId, int teamId)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                var player = await halisahaDbContext.Players.FindAsync(playerId);
                PlayerTeam playerTeam = new PlayerTeam();
                playerTeam.PlayerId = playerId;
                playerTeam.TeamId = teamId;
                await halisahaDbContext.PlayerTeams.AddAsync(playerTeam);
                await halisahaDbContext.SaveChangesAsync();
                return player;
            }
        }

        // public async Task<Player> PlayerJoinTeam(int playerId, int teamId)
        // {
        //     using (var halisahaDbContext = new HalisahaDbContext())
        //     {
        //         var player = await halisahaDbContext.Players.FindAsync(playerId);
        //         var team = await halisahaDbContext.Teams.FindAsync(teamId);
        //         if (player != null && team != null)
        //         {
        //             if (player.Teams == null)
        //             {
        //                 player.Teams = new List<Team> { team };
        //             }
        //             else
        //             {
        //                 player.Teams!.Add(team);
        //             }

        //             if (team.Players == null)
        //             {
        //                 team.Players = new List<Player> { player };
        //             }
        //             else
        //             {
        //                 team.Players!.Add(player);
        //             }

        //             await halisahaDbContext.SaveChangesAsync();

        //         }
        //         return player;
        //     }
        // }





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