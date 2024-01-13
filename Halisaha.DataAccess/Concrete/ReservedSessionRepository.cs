using System;
using System.Diagnostics.CodeAnalysis;
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
                halisahaDbContext.ReservedSessions.Remove(deletedReservedSession!);
                await halisahaDbContext.SaveChangesAsync();
                return deletedReservedSession!;
            }
        }

        public async Task<ReservedSession> GetReservedSessionById(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.ReservedSessions.FindAsync(id!);
            }
        }

        public async Task<List<ReservedSession>> GetReservedSessionsByOwnerId(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                List<Session> sessions = halisahaDbContext.Sessions.Where(x => x.OwnerId == id).Include(x => x.Owner).ToList();
                List<ReservedSession> reservedSessions = await halisahaDbContext.ReservedSessions.Where(x => sessions.Contains(x.Session)).Include(x => x.DeplasmanTakim).Include(x => x.EvSahibiTakim).ToListAsync();
                foreach (ReservedSession reservedSession in reservedSessions)
                {
                    if (reservedSession.DeplasmanTakim.ReservedSessions != null)
                    {
                        reservedSession.DeplasmanTakim.ReservedSessions = null;
                    }
                    if (reservedSession.EvSahibiTakim.ReservedSessions != null)
                    {
                        reservedSession.EvSahibiTakim.ReservedSessions = null;
                    }
                    reservedSession.Session.Owner.Sessions = null;
                }

                return reservedSessions;
            }
        }

        public async Task<List<ReservedSession>> GetReservedSessionsByPlayerId(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                List<Team> teams = await halisahaDbContext.PlayerTeams
                    .Where(x => x.PlayerId == id)
                    .Select(playerTeam => playerTeam.Team)
                    .ToListAsync();

                List<ReservedSession> reservedSessions = await halisahaDbContext.ReservedSessions.Where(x => teams.Contains(x.DeplasmanTakim) || teams.Contains(x.EvSahibiTakim)).Include(x => x.Session).ThenInclude(x => x.Owner).Include(x => x.DeplasmanTakim).Include(x => x.EvSahibiTakim).ToListAsync();

                foreach (ReservedSession reservedSession in reservedSessions)
                {
                    if (reservedSession.DeplasmanTakim.ReservedSessions != null)
                    {
                        reservedSession.DeplasmanTakim.ReservedSessions = null;
                    }
                    if (reservedSession.EvSahibiTakim.ReservedSessions != null)
                    {
                        reservedSession.EvSahibiTakim.ReservedSessions = null;
                    }

                    reservedSession.Session.Owner.Sessions = null;
                }

                return reservedSessions;
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

