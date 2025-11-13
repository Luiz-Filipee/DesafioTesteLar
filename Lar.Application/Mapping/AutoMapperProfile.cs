using AutoMapper;
using Lar.Domain.Entities;
using Lar.Application.DTOs;

namespace Lar.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Pessoa, PessoaDto>();
        CreateMap<CreatePessoaDto, Pessoa>();
        CreateMap<UpdatePessoaDto, Pessoa>();

        CreateMap<Telefone, TelefoneDto>();
        CreateMap<CreateTelefoneDto, Telefone>();
        CreateMap<UpdateTelefoneDto, Telefone>();

        CreateMap<Usuario, UsuarioDto>();
        CreateMap<CreateUsuarioDto, Usuario>();
        CreateMap<UpdateUsuarioDto, Usuario>();
    }
}
