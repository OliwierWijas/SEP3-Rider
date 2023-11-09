using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface ILoginLogic
{
    Task<UserBasicDTO> LoginAsync(LoginDTO dto);
}