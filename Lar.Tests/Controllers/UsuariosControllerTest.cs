using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Lar.API.Controllers;
using Lar.Application.Services;
using Lar.Application.DTOs;

public class UsuariosControllerTest
{
    private readonly Mock<IUsuarioService> _serviceMock;
    private readonly Mock<IValidator<CreateUsuarioDto>> _createValidatorMock;
    private readonly Mock<IValidator<UpdateUsuarioDto>> _updateValidatorMock;
    private readonly UsuariosController _controller;

    public UsuariosControllerTest()
    {
        _serviceMock = new Mock<IUsuarioService>();
        _createValidatorMock = new Mock<IValidator<CreateUsuarioDto>>();
        _updateValidatorMock = new Mock<IValidator<UpdateUsuarioDto>>();

        _createValidatorMock
            .Setup(v => v.Validate(It.IsAny<CreateUsuarioDto>()))
            .Returns(new FluentValidation.Results.ValidationResult());
        _updateValidatorMock
            .Setup(v => v.Validate(It.IsAny<UpdateUsuarioDto>()))
            .Returns(new FluentValidation.Results.ValidationResult());

        _controller = new UsuariosController(
            _serviceMock.Object,
            _createValidatorMock.Object,
            _updateValidatorMock.Object
        );
    }

    [Fact]
    public async Task GetById_DeveRetornarNotFound_QuandoUsuarioNaoExistir()
    {
        _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((UsuarioDto)null);
        var result = await _controller.GetById(1);
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
