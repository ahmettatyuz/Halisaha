using System;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Halisaha.DataAccess.Concrete
{
    public class ReservedSessionRepository : IReservedSessionRepository
    {
        public async Task<ReservedSession> CreateReservedSession(ReservedSession reservedSession)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                await halisahaDbContext.ReservedSessions.AddAsync(reservedSession);
                await halisahaDbContext.SaveChangesAsync();
                return reservedSession;
            }
        }

        public async Task<ReservedSession> DeleteReservedSession(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                var deletedReservedSession = await halisahaDbContext.ReservedSessions.FindAsync(id);
                halisahaDbContext.ReservedSessions.Remove(deletedReservedSession);
                await halisahaDbContext.SaveChangesAsync();
                return deletedReservedSession;
            }
        }

        public async Task<List<ReservedSession>> GetReservedSessionsByOwnerId(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.ReservedSessions.Where(x => x.Session.Owner.Id == id).ToListAsync();
                
            }
        }

        public async Task<List<ReservedSession>> GetReservedSessionsByPlayerId(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.ReservedSessions.Where(x => x.Session.Owner.Id == id).ToListAsync();
            }
        }

        public async Task<ReservedSession> UpdateReservedSession(ReservedSession reservedSession)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                halisahaDbContext.ReservedSessions.Update(reservedSession);
                await halisahaDbContext.SaveChangesAsync();
                return reservedSession;
            }
        }
    }
}

