using System;
using Halisaha.Entities;

namespace Halisaha.Business.Abstract
{
    public interface IReservedSessionService
    {
        Task<List<ReservedSession>> GetReservedSessionsByOwnerId(int id);

        Task<List<ReservedSession>> GetReservedSessionsByPlayerId(int id);

        Task<ReservedSession> CreateReservedSession(ReservedSession reservedSession);

        Task<ReservedSession> DeleteReservedSession(int id);

        Task<ReservedSession> UpdateReservedSession(ReservedSession reservedSession);
    }
}

