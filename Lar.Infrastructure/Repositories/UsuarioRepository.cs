using Lar.Domain.Entities;
using Lar.Domain.Interfaces;
using Lar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Lar.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _ctx;

    public UsuarioRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Usuario?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _ctx.Usuarios
            .Include(u => u.Pessoa)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }
}
