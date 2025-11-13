namespace Lar.Application.DTOs
{
    public record UsuarioResponseDto(
        int Id,
        string Username,
        int PessoaId
    );
}
