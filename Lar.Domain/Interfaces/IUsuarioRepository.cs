using Lar.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Lar.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}