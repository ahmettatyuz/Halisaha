using System;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Halisaha.DataAccess.Concrete
{
	public class TeamRepository : ITeamRepository
	{
        public async Task<Team> CreateTeam(Team team)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                await halisahaDbContext.Teams.AddAsync(team);
                await halisahaDbContext.SaveChangesAsync();
                return team;
            }
        }

        public async Task<Team> DeleteTeam(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                Team silinecekTeam = await halisahaDbContext.Teams.FindAsync(id);
                halisahaDbContext.Teams.Remove(silinecekTeam);
                await halisahaDbContext.SaveChangesAsync();
                return silinecekTeam;
            }
        }

        public async Task<Team> GetTeam(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Teams.Include(x=>x.Players).Where(x=>x.Id==id).FirstOrDefaultAsync();
            }
        }

        public async Task<List<Team>> GetTeams()
        {
            using(var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Teams.Include(x=>x.Players).ToListAsync();
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
    }
}

