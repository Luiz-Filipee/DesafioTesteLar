using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Lar.API.Controllers;
using Lar.Application.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Lar.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _configMock = new Mock<IConfiguration>();
            
            _configMock.Setup(c => c["Jwt:Key"]).Returns("estaEhUmaChaveMuitoSeguraComMaisDe32Caracteres!");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("TesteIssuer");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("TesteAudience");
            _configMock.Setup(c => c["Jwt:ExpireMinutes"]).Returns("60");

            _controller = new AuthController(_configMock.Object, _authServiceMock.Object);
        }

        [Fact(DisplayName = "Login retorna Ok com token quando credenciais corretas")]
        public async Task Login_RetornaOk_ComToken()
        {
            _authServiceMock.Setup(a => a.ValidateUserAsync("teste", "1234")).ReturnsAsync(true);

            var resultado = await _controller.Login(new LoginRequest("teste", "1234")) as OkObjectResult;

            Assert.NotNull(resultado);
            Assert.Contains("token", resultado.Value.ToString());
        }

        [Fact(DisplayName = "Login retorna NotFound quando credenciais incorretas")]
        public async Task Login_RetornaNotFound_QuandoCredenciaisInvalidas()
        {
            _authServiceMock.Setup(a => a.ValidateUserAsync("teste", "errado")).ReturnsAsync(false);

            var resultado = await _controller.Login(new LoginRequest("teste", "errado"));

            Assert.IsType<NotFoundObjectResult>(resultado);
        }
    }
}
