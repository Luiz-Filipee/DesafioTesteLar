namespace Lar.Domain.Entities;

public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
}