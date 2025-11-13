namespace Lar.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; } = null!;
}
