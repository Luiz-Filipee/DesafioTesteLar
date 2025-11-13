using Lar.Domain.Entities;
using Lar.Domain.Interfaces;

public interface IAuthService
{
    Task<bool> ValidateUserAsync(string username, string password);
}

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> ValidateUserAsync(string username, string password)
    {
        var usuario = await _usuarioRepository.GetByUsernameAsync(username);
        if (usuario == null) return false;

        return BCrypt.Net.BCrypt.Verify(password, usuario.Password);
    }
}