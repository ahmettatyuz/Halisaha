using System;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Halisaha.DataAccess.Concrete
{
    public class TeamRepository : ITeamRepository
    {
        public async Task<Team> CreateTeam(Team team)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                team.Deleted = 0;
                await halisahaDbContext.Teams.AddAsync(team);
                await halisahaDbContext.SaveChangesAsync();
                return team;
            }
        }
        public async Task<Team> DeleteTeam(int id, int playerId)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                Team silinecekTeam = await halisahaDbContext.Teams.FindAsync(id);
                if (silinecekTeam.CaptainPlayer == playerId)
                {
                    silinecekTeam.Deleted = 1;
                    halisahaDbContext.Teams.Update(silinecekTeam);
                }
                else
                {
                    PlayerTeam deletedPlayer = await halisahaDbContext.PlayerTeams.Where(x => x.TeamId == id && x.PlayerId == playerId).FirstOrDefaultAsync();
                    deletedPlayer.Deleted = 1;
                    halisahaDbContext.PlayerTeams.Update(deletedPlayer);
                }
                await halisahaDbContext.SaveChangesAsync();
                return silinecekTeam;
            }
        }

        public async Task<Team> GetTeam(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Teams.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<List<Team>> GetTeams()
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {

                return await halisahaDbContext.Teams.Where(x=>x.Deleted==0).ToListAsync();
            }
        }

        public async Task<List<Player>> GetPlayersInTeam(int teamId)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                List<Player> players = new List<Player>();
                List<PlayerTeam> playerTeams = await halisahaDbContext.PlayerTeams.Where(x => x.TeamId == teamId && x.Deleted==0).Include(x => x.Player).ToListAsync();
                foreach (PlayerTeam playerTeam in playerTeams)
                {
                    players.Add(playerTeam.Player);
                }

                return players;
            }
        }

        public async Task<Team> UpdateTeam(Team team)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                halisahaDbContext.Teams.Update(team);
                await halisahaDbContext.SaveChangesAsync();
                return team;
            }
        }

        public async Task<List<Team>> GetPlayersTeam(int playerId)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Teams.Where(x => x.CaptainPlayer == playerId && x.Deleted==0).ToListAsync();
            }
        }

        public async Task<List<Team>> GetTeamIncludedPlayer(int playerId)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                List<Team> teams = new();
                List<PlayerTeam> playerTeams = await halisahaDbContext.PlayerTeams.Where(x => x.PlayerId == playerId && x.Deleted == 0).ToListAsync();
                foreach (PlayerTeam playerTeam in playerTeams)
                {
                    Team team = await halisahaDbContext.Teams.FindAsync(playerTeam.TeamId);
                    if (team.Deleted == 0)
                    {
                        team.Players = await GetPlayersInTeam(playerTeam.TeamId);
                        teams.Add(team);
                    }
                }

                return teams;
            }
        }

        public async Task<bool> DeletePlayerFromTeam(int playerId, int teamId)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                PlayerTeam deletedPlayer = await halisahaDbContext.PlayerTeams.Where(x => x.TeamId == teamId && x.PlayerId == playerId).FirstOrDefaultAsync();
                if (deletedPlayer != null)
                {
                    halisahaDbContext.PlayerTeams.Remove(deletedPlayer);
                    return true;
                }
                return false;
            }
        }

    }
}

