using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Lar.API.Controllers;
using Lar.Application.Services;
using Lar.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lar.Tests.Controllers
{
    public class TelefonesControllerTests
    {
        private readonly Mock<ITelefoneService> _serviceMock;
        private readonly TelefonesController _controller;

        public TelefonesControllerTests()
        {
            _serviceMock = new Mock<ITelefoneService>();
            _controller = new TelefonesController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenTelefoneDoesNotExist()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((TelefoneDto?)null);
            var result = await _controller.GetById(1);
            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task GetByPessoa_ShouldReturnOk_WithListOfTelefones()
        {
            var lista = new List<TelefoneDto>
            {
                new TelefoneDto(1, "123456789", 1)
            };

            _serviceMock.Setup(s => s.GetByPessoaAsync(1)).ReturnsAsync(lista);

            var result = await _controller.GetByPessoa(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(lista, result.Value);
        }
    }
}
