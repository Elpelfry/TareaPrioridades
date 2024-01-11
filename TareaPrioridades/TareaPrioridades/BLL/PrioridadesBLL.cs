using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TareaPrioridades.DAL;
using TareaPrioridades.Models;

namespace TareaPrioridades.BLL
{
    public class PrioridadesBLL
    {
        private readonly Contexto _contex;

        public PrioridadesBLL(Contexto contexto)
        {
            _contex = contexto;
        }
        public bool Existe(int PrioridadId)
        {
            return _contex.Prioridades.Any(p => p.PrioridadId == PrioridadId);
        }

        public bool Insertar(Prioridades Prioridades)
        {
            _contex.Prioridades.Add(Prioridades);
            return _contex.SaveChanges() > 0;
        }

        public bool Modificar(Prioridades Prioridades)
        {
            var p = _contex.Prioridades.Find(Prioridades.PrioridadId);
            _contex.Entry(p!).State = EntityState.Detached;
            _contex.Entry(Prioridades).State = EntityState.Modified;
            return _contex.SaveChanges() > 0;
        }
        public bool Guardar(Prioridades Prioridades)
        {
            if (!Existe(Prioridades.PrioridadId))
                return this.Insertar(Prioridades);
            else
                return this.Modificar(Prioridades);
        }
        public bool Eliminar(Prioridades Prioridades)
        {
            var p = _contex.Prioridades.Find(Prioridades.PrioridadId);
            _contex.Entry(p!).State = EntityState.Detached;
            _contex.Entry(Prioridades).State = EntityState.Deleted;
            return _contex.SaveChanges() > 0;
        }

        public Prioridades? Buscar(int PrioridadId)
        {
            return _contex.Prioridades.AsNoTracking().SingleOrDefault(a => a.PrioridadId == PrioridadId);
        }
        public List<Prioridades> Listar(Expression<Func<Prioridades, bool>> Criterio)
        {
            return _contex.Prioridades.Where(Criterio).AsNoTracking().ToList();
        }
    }
}
