using Lar.Domain.Entities;
using Lar.Domain.Interfaces;
using Lar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lar.Infrastructure.Repositories;

public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
{ 
    public PessoaRepository(AppDbContext ctx) : base(ctx) { }
}
