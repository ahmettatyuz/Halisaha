using System;
using Halisaha.Business.Abstract;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;

namespace Halisaha.Business.Concrete
{
	public class ReservedSessionManager:IReservedSessionService
	{
		IReservedSessionRepository _reservedSessionRepository;
		public ReservedSessionManager(IReservedSessionRepository reservedSessionRepository)
		{
			_reservedSessionRepository = reservedSessionRepository;
		}

        public async Task<ReservedSession> CreateReservedSession(ReservedSession reservedSession)
        {
            return await _reservedSessionRepository.CreateReservedSession(reservedSession);
        }

        public async Task<ReservedSession> DeleteReservedSession(int id)
        {
            return await _reservedSessionRepository.DeleteReservedSession(id);
        }

        public async Task<ReservedSession> GetReservedSessionById(int id)
        {
            return await _reservedSessionRepository.GetReservedSessionById(id);
        }

        public async Task<List<ReservedSession>> GetReservedSessionsByOwnerId(int id)
        {
            return await _reservedSessionRepository.GetReservedSessionsByOwnerId(id);
        }

        public async Task<List<ReservedSession>> GetReservedSessionsByPlayerId(int id)
        {
            return await _reservedSessionRepository.GetReservedSessionsByPlayerId(id);
        }

        public async Task<ReservedSession> UpdateReservedSession(ReservedSession reservedSession)
        {
            return await _reservedSessionRepository.UpdateReservedSession(reservedSession);
        }
    }
}

//5 15 67 4 91 21 87 9 53 66