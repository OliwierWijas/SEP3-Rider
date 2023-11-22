using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class LoginLogic : ILoginLogic
{
    public async Task<UserBasicDTO> LoginAsync(LoginDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });

        var client = new LoginService.LoginServiceClient(channel);

        try
        {
            LoginResponse response = await client.LoginAsync(new LoginRequest()
            {
                Email = dto.Email,
                Password = dto.Password
            });
            UserBasicDTO user = new UserBasicDTO(response.Id, response.Email, response.Password, response.PhoneNumber, response.Address, response.Name, response.FirstName, response.LastName, response.Type);
            if (user == null)
                throw new Exception($"User with the given email {dto.Email} was not found.");
            return user;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
       

    }
}