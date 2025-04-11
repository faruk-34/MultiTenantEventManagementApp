using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tenant> Get(int id, CancellationToken cancellationToken)
        {
            return await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id,cancellationToken);
        }

        public async Task Insert(Tenant tenant, CancellationToken cancellationToken)
        {
             await _context.Tenants.AddAsync(tenant, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
 
        public async Task<IEnumerable<Tenant>> GetAll()
        {
             return await _context.Tenants.ToListAsync();
        }

        public async Task Update(Tenant tenant)
        {
             _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Tenant tenant)
        {
             _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();
        }
    }
}
