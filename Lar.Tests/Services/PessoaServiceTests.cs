using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Lar.Application.Services;
using Lar.Domain.Entities;
using Lar.Domain.Interfaces;
using Lar.Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Lar.Tests.Services
{
    public class PessoaServiceTests
    {
        private readonly Mock<IRepository<Pessoa>> _repoMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<PessoaService>> _loggerMock;
        private readonly PessoaService _service;

        public PessoaServiceTests()
        {
            _repoMock = new Mock<IRepository<Pessoa>>();
            _loggerMock = new Mock<ILogger<PessoaService>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pessoa, PessoaDto>();
                cfg.CreateMap<CreatePessoaDto, Pessoa>();
            });
            _mapper = config.CreateMapper();

            _service = new PessoaService(_repoMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact(DisplayName = "Criar pessoa retorna PessoaDto")]
        public async Task CriarPessoa_RetornaPessoaDto()
        {
            var dto = new CreatePessoaDto("João", "joao@email.com");
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Pessoa>(), default))
                     .ReturnsAsync((Pessoa p, CancellationToken _) => p);

            var resultado = await _service.CreateAsync(dto);

            Assert.Equal(dto.Nome, resultado.Nome);
            Assert.Equal(dto.Email, resultado.Email);
        }

        [Fact(DisplayName = "Obter pessoa por ID retorna null quando não existe")]
        public async Task ObterPessoaPorId_RetornaNull_QuandoNaoExiste()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync((Pessoa)null);

            var resultado = await _service.GetByIdAsync(1);

            Assert.Null(resultado);
        }

        [Fact(DisplayName = "Obter todas as pessoas retorna lista de PessoaDto")]
        public async Task ObterTodasAsPessoas_RetornaListaDePessoaDto()
        {
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" }
            };

            _repoMock.Setup(r => r.GetAllWithIncludesAsync(It.IsAny<System.Func<IQueryable<Pessoa>, IQueryable<Pessoa>>>(), default))
                .ReturnsAsync(pessoas);

            var resultado = await _service.GetAllAsync();

            Assert.Single(resultado);
            Assert.Equal("João", resultado.First().Nome);
        }
    }
}
