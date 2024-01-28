using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TareaPrioridades.DAL;
using TareaPrioridades.Models;

namespace TareaPrioridades.Services
{
    public class SistemasService
    {
        private readonly Contexto _contexto;

        public SistemasService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Guardar(Sistemas sistema)
        {
            if (_contexto.Sistemas!.Any(s => s.NombreSistema!.ToLower().Replace(" ", "") == sistema.NombreSistema!.ToLower().Replace(" ", "")
            && s.SistemaId != sistema.SistemaId))
            {
                return false;
            }
            if (!await Existe(sistema.SistemaId))
                return await Insertar(sistema);
            else
                return await Modificar(sistema);
        }

        public async Task<bool> Insertar(Sistemas sistema)
        {
            _contexto.Add(sistema);
            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Sistemas sistema)
        {
            _contexto.Update(sistema);
            int cantidad = await _contexto.SaveChangesAsync();
            _contexto.Entry(sistema).State = EntityState.Detached;
            return cantidad > 0;
        }
        
        public async Task<bool> Existe(int SistemaId)
        {
            return await _contexto.Sistemas!
                .AnyAsync(s => s.SistemaId == SistemaId);
        }

        public async Task<bool> Eliminar(Sistemas sistema)
        {
            var cantidad = await _contexto.Sistemas!
                .Where(s => s.SistemaId == sistema.SistemaId)
                .ExecuteDeleteAsync();
            return cantidad > 0;
        }

        public async Task<Sistemas?> Buscar(int SistemaId)
        {
            return await _contexto.Sistemas!
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SistemaId == SistemaId);
        }

        public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
        {
            return await _contexto.Sistemas!
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
