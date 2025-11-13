using Lar.Application.DTOs;

namespace Lar.Application.Services;

public interface IPessoaService
{
    Task<PessoaDto> CreateAsync(CreatePessoaDto dto);
    Task<IEnumerable<PessoaDto>> GetAllAsync();
    Task<PessoaDto?> GetByIdAsync(int id);
    Task UpdateAsync(int id, UpdatePessoaDto dto);
    Task DeleteAsync(int id);
}
