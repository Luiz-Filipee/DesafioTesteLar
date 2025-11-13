using AutoMapper;
using Lar.Domain.Entities;
using Lar.Domain.Interfaces;
using Lar.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lar.Application.Services;

public class PessoaService : IPessoaService
{
    private readonly IRepository<Pessoa> _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<PessoaService> _logger;

    public PessoaService(IRepository<Pessoa> repo, IMapper mapper, ILogger<PessoaService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PessoaDto> CreateAsync(CreatePessoaDto dto)
    {
        var entity = new Pessoa { Nome = dto.Nome, Email = dto.Email };
        
        var created = await _repo.AddAsync(entity);

        _logger.LogInformation("Pessoa {Nome} criada com sucesso", dto.Nome);   

        return _mapper.Map<PessoaDto>(created);
    }


    public async Task<IEnumerable<PessoaDto>> GetAllAsync()
    {
        var pessoas = await _repo.GetAllWithIncludesAsync(
            q => q.Include(p => p.Telefones)
        );

        _logger.LogInformation("{Count} pessoas encontrados", pessoas.Count());
        
        return _mapper.Map<IEnumerable<PessoaDto>>(pessoas);
    }

    public async Task<PessoaDto?> GetByIdAsync(int id)
    {
        var p = await _repo.GetByIdAsync(id);

        if (p != null)
            _logger.LogInformation("Pessoa {Id} encontrado", id);
        else
            _logger.LogWarning("Pessoa {Id} não encontrado", id);
        
        return p == null ? null : _mapper.Map<PessoaDto>(p);
    }

    public async Task UpdateAsync(int id, UpdatePessoaDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
       
        if (entity == null)
        {
            _logger.LogWarning("Tentativa de atualizar pessoa não existente: ID {Id}", id);
            throw new KeyNotFoundException("Pessoa não encontrado");
        }
        
        entity.Nome = dto.Nome;
        entity.Email = dto.Email;
        
        await _repo.UpdateAsync(entity);
        _logger.LogInformation("Pessoa {Nome} atualizada com sucesso", dto.Nome);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
        _logger.LogInformation("Pessoa ID {Id} deletado", id);
    }
}
