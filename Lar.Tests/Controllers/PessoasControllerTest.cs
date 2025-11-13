using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Lar.API.Controllers;
using Lar.Application.Services;
using Lar.Application.DTOs;

public class PessoasControllerTest
{
    private readonly Mock<IPessoaService> _serviceMock;
    private readonly Mock<IValidator<CreatePessoaDto>> _createValidatorMock;
    private readonly Mock<IValidator<UpdatePessoaDto>> _updateValidatorMock;
    private readonly PessoasController _controller;

    public PessoasControllerTest()
    {
        _serviceMock = new Mock<IPessoaService>();
        _createValidatorMock = new Mock<IValidator<CreatePessoaDto>>();
        _updateValidatorMock = new Mock<IValidator<UpdatePessoaDto>>();

        _createValidatorMock
            .Setup(v => v.Validate(It.IsAny<CreatePessoaDto>()))
            .Returns(new FluentValidation.Results.ValidationResult());
        _updateValidatorMock
            .Setup(v => v.Validate(It.IsAny<UpdatePessoaDto>()))
            .Returns(new FluentValidation.Results.ValidationResult());

        _controller = new PessoasController(
            _serviceMock.Object,
            _createValidatorMock.Object,
            _updateValidatorMock.Object
        );
    }

    [Fact]
    public async Task GetById_DeveRetornarNotFound_QuandoPessoaNaoExistir()
    {
        _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((PessoaDto)null);
        var result = await _controller.GetById(1);
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
