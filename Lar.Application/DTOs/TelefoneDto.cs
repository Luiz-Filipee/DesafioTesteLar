namespace Lar.Application.DTOs;

public record TelefoneDto(int Id, string Numero, int PessoaId);
public record CreateTelefoneDto(string Numero, int PessoaId);
public record UpdateTelefoneDto(string Numero);
