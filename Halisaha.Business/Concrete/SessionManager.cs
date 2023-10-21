using System;
using Halisaha.Business.Abstract;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;

namespace Halisaha.Business.Concrete
{
	public class SessionManager:ISessionService
	{
		private ISessionRepository _sessionRepository;
		public SessionManager(ISessionRepository sessionRepository)
		{
			_sessionRepository = sessionRepository;
		}

        public async Task<Session> CreateSession(Session session)
        {
            return await _sessionRepository.CreateSession(session);
        }

        public async Task<Session> DeleteSession(int id)
        {
            return await _sessionRepository.DeleteSession(id);
        }

        public async Task<List<Session>> GetSessionsByOwnerId(int ownerId)
        {
            return await _sessionRepository.GetSessionsByOwnerId(ownerId);
        }

        public async Task<Session> UpdateSession(Session session)
        {
            return await _sessionRepository.UpdateSession(session);
        }
    }
}

