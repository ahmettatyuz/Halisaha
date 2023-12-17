using System;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Halisaha.DataAccess.Concrete
{
    public class SessionRepository : ISessionRepository
    {
        public async Task<Session> CreateSession(Session session)
        {
            using(var halisahaDbContext = new HalisahaDbContext())
            {
                halisahaDbContext.Sessions.Add(session);
                await halisahaDbContext.SaveChangesAsync();
                return session;
            }
        }

        public async Task<Session> DeleteSession(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                var deletedSession = halisahaDbContext.Sessions.Find(id);
                halisahaDbContext.Sessions.Remove(deletedSession);
                await halisahaDbContext.SaveChangesAsync();
                return deletedSession;
            }
        }

        public async Task<Session> GetSessionById(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Sessions.Where(x => x.Id != id).Include(x => x.Owner).FirstOrDefaultAsync();
            }
        }

        public async Task<List<Session>> GetSessionsByOwnerId(int ownerId)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Sessions.Where(x => x.Owner.Id == ownerId).Include(x=>x.Owner).ToListAsync();
            }
        }

        public async Task<Session> UpdateSession(Session session)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                halisahaDbContext.Sessions.Update(session);
                await halisahaDbContext.SaveChangesAsync();
                return session;
            }
        }
    }
}

