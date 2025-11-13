using Microsoft.AspNetCore.Mvc;
using Lar.Application.Services;
using Lar.Application.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _service;
    private readonly IValidator<CreatePessoaDto> _createValidator;
    private readonly IValidator<UpdatePessoaDto> _updateValidator;

    public PessoasController(IPessoaService service,
        IValidator<CreatePessoaDto> createValidator,
        IValidator<UpdatePessoaDto> updateValidator)
    {
        _service = service;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _service.GetByIdAsync(id);
        
        if (p == null)  
            return NotFound($"Pessoa {id} não encontrada");
        
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePessoaDto dto)
    {
        var val = await _createValidator.ValidateAsync(dto);
        
        if (!val.IsValid) return BadRequest(val.Errors.Select(e => e.ErrorMessage));
        
        var created = await _service.CreateAsync(dto);
        
        var response = new PessoaResponseDto(created.Id, created.Nome, created.Email);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePessoaDto dto)
    {
        var val = await _updateValidator.ValidateAsync(dto);
        
        if (!val.IsValid) 
            return BadRequest(val.Errors.Select(e => e.ErrorMessage));
        
        await _service.UpdateAsync(id, dto);

        var updated = await _service.GetByIdAsync(id);
        var response = new PessoaResponseDto(updated.Id, updated.Nome, updated.Email);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new { mensagem = $"Pessoa com ID {id} deletado com sucesso." });
    }
}
