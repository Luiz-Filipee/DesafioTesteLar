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
    public class TelefoneServiceTests
    {
        private readonly Mock<IRepository<Telefone>> _repoMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<TelefoneService>> _loggerMock;
        private readonly TelefoneService _service;

        public TelefoneServiceTests()
        {
            _repoMock = new Mock<IRepository<Telefone>>();
            _loggerMock = new Mock<ILogger<TelefoneService>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Telefone, TelefoneDto>();
                cfg.CreateMap<CreateTelefoneDto, Telefone>();
            });
            _mapper = config.CreateMapper();

            _service = new TelefoneService(_repoMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact(DisplayName = "Criar telefone retorna TelefoneDto")]
        public async Task CriarTelefone_RetornaTelefoneDto()
        {
            var dto = new CreateTelefoneDto("12345678", 1);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Telefone>(), default))
                     .ReturnsAsync((Telefone t, CancellationToken _) => t);

            var resultado = await _service.CreateAsync(dto);

            Assert.Equal(dto.Numero, resultado.Numero);
            Assert.Equal(dto.PessoaId, resultado.PessoaId);
        }

        [Fact(DisplayName = "Obter telefone por ID retorna null quando não existe")]
        public async Task ObterTelefonePorId_RetornaNull_QuandoNaoExiste()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync((Telefone)null);

            var resultado = await _service.GetByIdAsync(1);

            Assert.Null(resultado);
        }

        [Fact(DisplayName = "Obter telefones por pessoa retorna lista de TelefoneDto")]
        public async Task ObterTelefonesPorPessoa_RetornaListaDeTelefoneDto()
        {
            var telefones = new List<Telefone>
            {
                new Telefone { Id = 1, Numero = "12345678", PessoaId = 1 }
            };

            _repoMock.Setup(r => r.GetAllAsync(default)).ReturnsAsync(telefones);

            var resultado = await _service.GetByPessoaAsync(1);

            Assert.Single(resultado);
            Assert.Equal("12345678", resultado.First().Numero);
        }
    }
}
