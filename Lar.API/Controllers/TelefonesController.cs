using Microsoft.AspNetCore.Mvc;
using Lar.Application.Services;
using Lar.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Lar.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TelefonesController : ControllerBase
{
    private readonly ITelefoneService _service;

    public TelefonesController(ITelefoneService service)
    {
        _service = service;
    }

    [HttpGet("pessoa/{pessoaId:int}")]
    public async Task<IActionResult> GetByPessoa(int pessoaId)
    {
        var telefones = await _service.GetByPessoaAsync(pessoaId);
        
        return Ok(telefones);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var telefone = await _service.GetByIdAsync(id);
        
        if (telefone == null) 
            throw new KeyNotFoundException($"Telefone {id} não encontrada");
        
        return Ok(telefone);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTelefoneDto dto)
    {
        var created = await _service.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTelefoneDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        
        return NoContent();
    }
}
