using Lar.Application.DTOs;

namespace Lar.Application.Services;

public interface IUsuarioService
{
    Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);
    Task UpdateAsync(int id, UpdateUsuarioDto dto);
    Task DeleteAsync(int id);
    Task<UsuarioDto?> GetByIdAsync(int id);
}
