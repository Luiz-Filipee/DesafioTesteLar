using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Lar.Application.Services;
using Lar.Domain.Entities;
using Lar.Domain.Interfaces;
using Lar.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Lar.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IRepository<Usuario>> _repoMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UsuarioService>> _loggerMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _repoMock = new Mock<IRepository<Usuario>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioDto>();
                cfg.CreateMap<CreateUsuarioDto, Usuario>();
            });

            _mapper = config.CreateMapper();
            _loggerMock = new Mock<ILogger<UsuarioService>>();
            _service = new UsuarioService(_repoMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact(DisplayName = "Criar usuário deve gerar hash da senha e retornar DTO")]
        public async Task CriarUsuario_DeveGerarHashDaSenhaERetornarDto()
        {
            var dto = new CreateUsuarioDto("teste", "123456", 1);

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Usuario>(), default))
                .ReturnsAsync((Usuario u, CancellationToken _) => u);

            var resultado = await _service.CreateAsync(dto);

            Assert.Equal(dto.Username, resultado.Username);
            Assert.NotEqual(dto.Password, resultado.Password);
        }

        [Fact(DisplayName = "Obter usuário por ID retorna nulo quando não encontrado")]
        public async Task ObterUsuarioPorId_RetornaNulo_QuandoNaoEncontrado()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync((Usuario)null);
            var resultado = await _service.GetByIdAsync(1);
            Assert.Null(resultado);
        }
    }
}
