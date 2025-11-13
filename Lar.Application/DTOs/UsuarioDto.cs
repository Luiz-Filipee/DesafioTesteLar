namespace Lar.Application.DTOs;

public record UsuarioDto(int Id, string Username, string Password, int PessoaId);
public record CreateUsuarioDto(string Username, string Password, int PessoaId);
public record UpdateUsuarioDto(string Username, string Password);
