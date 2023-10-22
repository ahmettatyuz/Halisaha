using System;
using Halisaha.Entities;

namespace Halisaha.DataAccess.Abstract
{
	public interface IOwnerRepository
	{
		Task<Owner> GetOwnerById(int id);

        Task<Owner> GetOwnerByPhone(string phone);

        Task<Owner> DeleteOwner(int id);

        Task<Owner> CreateOwner(Owner owner);

        Task<Owner> UpdateOwner(Owner owner);

        Task<List<Owner>> GetAllOwners();
    }
}

