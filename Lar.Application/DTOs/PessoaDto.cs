namespace Lar.Application.DTOs;

public record PessoaDto(int Id, string Nome, string Email, List<TelefoneDto> Telefones);
public record CreatePessoaDto(string Nome, string Email);
public record UpdatePessoaDto(string Nome, string Email);
