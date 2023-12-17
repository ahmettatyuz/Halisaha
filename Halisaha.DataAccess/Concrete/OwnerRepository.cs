using System;
using Halisaha.DataAccess.Abstract;
using Halisaha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Halisaha.DataAccess.Concrete
{
    public class OwnerRepository : IOwnerRepository
    {
        public async Task<Owner> CreateOwner(Owner owner)
        {
            using(var halisahaDbContext = new HalisahaDbContext())
            {
                halisahaDbContext.Owners.Add(owner);
                await halisahaDbContext.SaveChangesAsync();
                return owner;
            }
        }

        public async Task<Owner> DeleteOwner(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                var deletedOwner = await halisahaDbContext.Owners.FindAsync(id);
                halisahaDbContext.Owners.Remove(deletedOwner);
                await halisahaDbContext.SaveChangesAsync();
                return deletedOwner;
            }
        }

        public async Task<List<Owner>> GetAllOwners()
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Owners.Include(x=>x.Sessions).ToListAsync();
            }
        }

        public async Task<Owner> GetOwnerById(int id)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return await halisahaDbContext.Owners.FindAsync(id);
            }
        }

        public async Task<Owner> GetOwnerByPhone(string phone)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                return halisahaDbContext.Owners.Include(x=>x.Sessions).FirstOrDefault(x => x.Phone == phone);
            }
        }

        public async Task<Owner> UpdateOwner(Owner owner)
        {
            using (var halisahaDbContext = new HalisahaDbContext())
            {
                halisahaDbContext.Owners.Update(owner);
                await halisahaDbContext.SaveChangesAsync();
                return owner;
            }
        }
    }
}

