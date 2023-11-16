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

        var client = new LoginService.CustomerServiceClient(channel);

        try
        {
            UserBasicDTO? user = await client.LoginAsync(dto);
            if (user == null)
                throw new Exception($"User with the given email {dto.Email} was not found.");
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
     
    }
}