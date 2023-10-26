using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class CustomerLogic : ICustomerLogic
{
    public Task CreateAsync(CustomerCreationDTO customerDTO)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new CustomerService.CustomerServiceClient(channel);
        var reply = client.CreateCustomer(new CreateCustomerRequest
        {
            FirstName = customerDTO.FirstName,
            LastName = customerDTO.LastName,
            PhoneNumber = customerDTO.PhoneNumber,
            Email = customerDTO.Email,
            Password = customerDTO.Password
        });
        return Task.FromResult(reply);
    }
    
    
}