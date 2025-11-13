using AutoMapper;
using Lar.Domain.Entities;
using Lar.Domain.Interfaces;
using Lar.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace Lar.Application.Services;

public class TelefoneService : ITelefoneService
{
    private readonly IRepository<Telefone> _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<TelefoneService> _logger;

    public TelefoneService(IRepository<Telefone> repo, IMapper mapper, ILogger<TelefoneService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TelefoneDto> CreateAsync(CreateTelefoneDto dto)
    {
        var telefone = new Telefone
        {
            Numero = dto.Numero,
            PessoaId = dto.PessoaId
        };
        
        var created = await _repo.AddAsync(telefone);

        _logger.LogInformation("Telefone {Numero} criado para a pessoa {PessoaId}", dto.Numero, dto.PessoaId);
        
        return _mapper.Map<TelefoneDto>(created);
    }

    public async Task<IEnumerable<TelefoneDto>> GetByPessoaAsync(int pessoaId)
    {
        var all = await _repo.GetAllAsync();
        
        var telefones = all.Where(t => t.PessoaId == pessoaId);

        _logger.LogInformation("{Count} telefones encontrados para a pessoa {PessoaId}", telefones.Count(), pessoaId);
        
        return _mapper.Map<IEnumerable<TelefoneDto>>(telefones);
    }

    public async Task<TelefoneDto?> GetByIdAsync(int id)
    {
        var t = await _repo.GetByIdAsync(id);
       
        if (t != null)
            _logger.LogInformation("Telefone {Id} encontrado", id);
        else
            _logger.LogWarning("Telefone {Id} não encontrado", id);

        return t == null ? null : _mapper.Map<TelefoneDto>(t);
    }

    public async Task UpdateAsync(int id, UpdateTelefoneDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        
        if (entity == null)
        {
            _logger.LogWarning("Tentativa de atualizar telefone {Id} falhou: não encontrado", id);
            throw new KeyNotFoundException("Telefone não encontrado");
        }
        
        entity.Numero = dto.Numero;
        
        await _repo.UpdateAsync(entity);

        _logger.LogInformation("Telefone {Id} atualizado para {Numero}", id, dto.Numero);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
        _logger.LogInformation("Telefone {Id} deletado", id);
    }
}
