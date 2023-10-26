using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class CustomerLogic : ICustomerLogic
{
    public async Task CreateAsync(CustomerCreationDTO dto)
    {
        var client = GetClientConnection();
        var reply = await client.CreateCustomerAsync(new CreateCustomerRequest
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Password = dto.Password
        });
    }

    public async Task UpdateEmail(CustomerUpdateDTO dto)
    {
        var client = GetClientConnection();
        var reply = await client.UpdateEmailAsync(new UpdateEmailRequest
        {
            AccountId = dto.AccountId,
            Email = dto.Email
        });
    }

    public async Task UpdatePassword(CustomerUpdateDTO dto)
    {
        var client = GetClientConnection();
        await client.UpdatePasswordAsync(new UpdatePasswordRequest
        {
            AccountId = dto.AccountId,
            Password = dto.Password
        });
    }

    private static CustomerService.CustomerServiceClient GetClientConnection()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new CustomerService.CustomerServiceClient(channel);
        return client;
    }
}