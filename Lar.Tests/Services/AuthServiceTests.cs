using Xunit;
using Moq;
using Lar.Application.Services;
using Lar.Domain.Entities;
using Lar.Domain.Interfaces;
using System.Threading.Tasks;

namespace Lar.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _usuarioRepoMock = new Mock<IUsuarioRepository>();
            _authService = new AuthService(_usuarioRepoMock.Object);
        }

        [Fact(DisplayName = "Validar usuário retorna verdadeiro quando a senha confere")]
        public async Task ValidarUsuario_RetornaVerdadeiro_QuandoSenhaConfere()
        {
            var usuario = new Usuario
            {
                Username = "teste",
                Password = BCrypt.Net.BCrypt.HashPassword("1234")
            };

            _usuarioRepoMock.Setup(r => r.GetByUsernameAsync("teste", default))
                            .ReturnsAsync(usuario);

            var resultado = await _authService.ValidateUserAsync("teste", "1234");
            Assert.True(resultado);
        }

        [Fact(DisplayName = "Validar usuário retorna falso quando a senha não confere")]
        public async Task ValidarUsuario_RetornaFalso_QuandoSenhaNaoConfere()
        {
            var usuario = new Usuario
            {
                Username = "teste",
                Password = BCrypt.Net.BCrypt.HashPassword("1234")
            };

            _usuarioRepoMock.Setup(r => r.GetByUsernameAsync("teste", default))
                .ReturnsAsync(usuario);

            var resultado = await _authService.ValidateUserAsync("teste", "senhaErrada");
            Assert.False(resultado);
        }
    }
}
