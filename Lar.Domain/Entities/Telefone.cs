namespace Lar.Domain.Entities;

public class Telefone
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    
    public int PessoaId { get; set; }
    public Pessoa? Pessoa { get; set; }
}