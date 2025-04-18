﻿using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAll(CancellationToken cancellationToken)
        {
            var query = _context.Events.AsQueryable();

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Event> Get(int id, CancellationToken cancellationToken)
        {
            return await _context.Events
     .Include(e => e.Registrations)
     .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        }

        public async Task Insert(Event eventEntity, CancellationToken cancellationToken)
        {
            await _context.Events.AddAsync(eventEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Event eventEntity, CancellationToken cancellationToken)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity != null)
            {
                _context.Events.Remove(eventEntity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }

}
