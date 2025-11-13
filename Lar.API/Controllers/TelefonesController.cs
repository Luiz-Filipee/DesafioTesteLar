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
            return NotFound($"Telefone {id} não encontrada");
        
        return Ok(telefone);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTelefoneDto dto)
    {
        var created = await _service.CreateAsync(dto);
        
        var response = new TelefoneResponseDto(created.Id, created.Numero, created.PessoaId);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTelefoneDto dto)
    {
        await _service.UpdateAsync(id, dto);

        var updated = await _service.GetByIdAsync(id);
        var response = new TelefoneResponseDto(updated.Id, updated.Numero, updated.PessoaId);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        
        return Ok(new { mensagem = $"Telefone com ID {id} deletado com sucesso." });
    }
}
