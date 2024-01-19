﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TareaPrioridades.DAL;
using TareaPrioridades.Models;

namespace TareaPrioridades.BLL;

public class ClientesBLL
{
    private readonly Contexto _contexto;

    public ClientesBLL(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Guardar(Clientes cliente)
    {
        //if (_contexto.Clientes!.Any(c => c.Nombre!.ToLower().Replace(" ", "") == cliente.Nombre!.ToLower().Replace(" ", "")
        //&& c.ClienteId != cliente.ClienteId))
        //{
        //    return false;
        //}
        //if (_contexto.Clientes!.Any(c => c.RNC!.ToLower() == cliente.RNC!.ToLower() && c.ClienteId != cliente.ClienteId))
        //{
        //    return false;
        //}
        if (!await Existe(cliente.ClienteId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    public async Task<bool> Insertar(Clientes cliente)
    {
        _contexto.Add(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Clientes cliente)
    {
        _contexto.Update(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int ClienteId)
    {
        return await _contexto.Clientes!
            .AnyAsync(c => c.ClienteId == ClienteId);
    }

    public async Task<bool> Eliminar(Clientes cliente)
    {
        var cantidad = await _contexto.Clientes!
            .Where(p => p.ClienteId == cliente.ClienteId)
            .ExecuteDeleteAsync();

        return cantidad > 0;
    }

    public async Task<Clientes?> Buscar(int ClienteId)
    {
        return await _contexto.Clientes!
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ClienteId == ClienteId);
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        return await _contexto.Clientes!
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
