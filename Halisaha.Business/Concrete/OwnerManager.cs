using System;
using Halisaha.Business.Abstract;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;

namespace Halisaha.Business.Concrete
{
    public class OwnerManager : IOwnerService
    {
        //8703
        private IOwnerRepository _ownerRepository;
        public OwnerManager(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        public async Task<Owner> CreateOwner(Owner owner)
        {
            return await _ownerRepository.CreateOwner(owner);
        }

        public async Task<Owner> DeleteOwner(int id)
        {
            return await _ownerRepository.DeleteOwner(id);
        }

        public async Task<Owner> GetOwnerById(int id)
        {
            return await _ownerRepository.GetOwnerById(id);
        }

        public async Task<Owner> GetOwnerByPhone(string phone)
        {
            return await _ownerRepository.GetOwnerByPhone(phone);
        }

        public async Task<Owner> UpdateOwner(Owner owner)
        {
            return await _ownerRepository.UpdateOwner(owner);
        }
    }
}

