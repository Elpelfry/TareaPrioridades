using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TareaPrioridades.DAL;
using TareaPrioridades.Models;

namespace TareaPrioridades.Services
{
    public class TicketsService
    {
        private readonly Contexto _contexto;

        public TicketsService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Guardar(Tickets ticket)
        {
            if (!await Existe(ticket.TicketId))
                return await Insertar(ticket);
            else
                return await Modificar(ticket);
        }

        public async Task<bool> Insertar(Tickets ticket)
        {
            _contexto.Add(ticket);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Tickets ticket)
        {
            _contexto.Update(ticket);
            int cantidad = await _contexto.SaveChangesAsync();
            _contexto.Entry(ticket).State = EntityState.Detached;
            return cantidad > 0;
        }

        public async Task<bool> Existe(int ticketId)
        {
            return await _contexto.Tickets!
                .AnyAsync(t => t.TicketId == ticketId);
        }

        public async Task<bool> Eliminar(Tickets ticket)
        {
            var cantidad = await _contexto.Tickets!
                .Where(t => t.TicketId == ticket.TicketId)
                .ExecuteDeleteAsync();
            return cantidad > 0;
        }

        public async Task<Tickets?> Buscar(int TicketId)
        {
            return await _contexto.Tickets!
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TicketId == TicketId);
        }

        public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
        {
            return await _contexto.Tickets!
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
