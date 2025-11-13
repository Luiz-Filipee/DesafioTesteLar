using Lar.Application.DTOs;

namespace Lar.Application.Services;

public interface ITelefoneService
{
    Task<TelefoneDto> CreateAsync(CreateTelefoneDto dto);
    Task<IEnumerable<TelefoneDto>> GetByPessoaAsync(int pessoaId);
    Task<TelefoneDto?> GetByIdAsync(int id);
    Task UpdateAsync(int id, UpdateTelefoneDto dto);
    Task DeleteAsync(int id);
}
