using System;
using Halisaha.Entities;

namespace Halisaha.DataAccess.Abstract
{
	public interface ISessionRepository
	{
		Task<List<Session>> GetSessionsByOwnerId(int ownerId);

		Task<Session> CreateSession(Session session);

        Task<Session> DeleteSession(int id);

        Task<Session> UpdateSession(Session session);
	}
}

