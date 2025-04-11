using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _context;

        public RegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Registration>> GetByEventIdAsync(int eventId, CancellationToken cancellationToken)
        {
            return await _context.Registrations
                .Where(r => r.EventId == eventId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Registration> Get (int eventId, int id, CancellationToken cancellationToken)
        {
            return await _context.Registrations
                .FirstOrDefaultAsync(r => r.EventId == eventId && r.Id == id, cancellationToken);
        }

        public async Task Insert(Registration registration, CancellationToken cancellationToken)
        {
            await _context.Registrations.AddAsync(registration, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update (Registration registration, CancellationToken cancellationToken)
        {
            _context.Registrations.Update(registration);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete (int eventId, int id, CancellationToken cancellationToken)
        {
            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.EventId == eventId && r.Id == id, cancellationToken);

            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }

}
