using AutoMapper;
using Lar.Domain.Entities;
using Lar.Domain.Interfaces;
using Lar.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace Lar.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IRepository<Usuario> _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<UsuarioService> _logger;

    public UsuarioService(IRepository<Usuario> repo, IMapper mapper, ILogger<UsuarioService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
    {
        var usuario = new Usuario
        {
            Username = dto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password), 
            PessoaId = dto.PessoaId
        };

        var created = await _repo.AddAsync(usuario);

        _logger.LogInformation("Usuário {Username} criado com sucesso", dto.Username);

        return _mapper.Map<UsuarioDto>(created);
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id)
    {
        var u = await _repo.GetByIdAsync(id);
        return u == null ? null : _mapper.Map<UsuarioDto>(u);
    }

    public async Task UpdateAsync(int id, UpdateUsuarioDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogWarning("Tentativa de atualizar usuário não existente: ID {Id}", id);
            throw new KeyNotFoundException("Usuário não encontrado");
        }

        entity.Username = dto.Username;

        if (!string.IsNullOrEmpty(dto.Password))
        {
            entity.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }

        await _repo.UpdateAsync(entity);
        _logger.LogInformation("Usuário {Username} atualizado com sucesso", dto.Username);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
        _logger.LogInformation("Usuário ID {Id} deletado", id);
    }

    public async Task<bool> ValidateUserAsync(string username, string password)
    {
        var usuario = (await _repo.GetAllAsync())
            .FirstOrDefault(u => u.Username == username);

        if (usuario == null)
        {
            _logger.LogWarning("Tentativa de login falhou: usuário {Username} não encontrado", username);
            return false;
        }

        var isValid = BCrypt.Net.BCrypt.Verify(password, usuario.Password);

        if (!isValid)
            _logger.LogWarning("Tentativa de login falhou: senha incorreta para {Username}", username);
        else
            _logger.LogInformation("Usuário {Username} autenticado com sucesso", username);

        return isValid;
    }
}
