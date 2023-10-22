using System;
using Halisaha.Entities;

namespace Halisaha.DataAccess.Abstract
{
	public interface IReservedSessionRepository
	{
        Task<ReservedSession> GetReservedSessionById(int id);

		Task<List<ReservedSession>> GetReservedSessionsByOwnerId(int id);

        Task<List<ReservedSession>> GetReservedSessionsByPlayerId(int id);

        Task<ReservedSession> CreateReservedSession(ReservedSession reservedSession);
		
        Task<ReservedSession> DeleteReservedSession(int id);

        Task<ReservedSession> UpdateReservedSession(ReservedSession reservedSession);
    }
}

