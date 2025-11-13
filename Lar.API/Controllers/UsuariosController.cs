using Microsoft.AspNetCore.Mvc;
using Lar.Application.Services;
using Lar.Application.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;
    private readonly IValidator<CreateUsuarioDto> _createValidator;
    private readonly IValidator<UpdateUsuarioDto> _updateValidator;

    public UsuariosController(IUsuarioService service,
        IValidator<CreateUsuarioDto> createValidator,
        IValidator<UpdateUsuarioDto> updateValidator)
    {
        _service = service;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
    {
        var val = await _createValidator.ValidateAsync(dto);
        
        if (!val.IsValid) 
            return BadRequest(val.Errors.Select(e => e.ErrorMessage));
        
        var created = await _service.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _service.GetByIdAsync(id);
        
        if (usuario == null) 
            return NotFound($"Usuário {id} não encontrada");

        var response = new UsuarioResponseDto(usuario.Id, usuario.Username, usuario.PessoaId);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUsuarioDto dto)
    {
        var val = await _updateValidator.ValidateAsync(dto);
        
        if (!val.IsValid) 
            return BadRequest(val.Errors.Select(e => e.ErrorMessage));
        
        await _service.UpdateAsync(id, dto);

        var updated = await _service.GetByIdAsync(id);
        var response = new UsuarioResponseDto(updated.Id, updated.Username, updated.PessoaId);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        
        return Ok(new { mensagem = $"Usuário com ID {id} deletado com sucesso." });
    }
}
