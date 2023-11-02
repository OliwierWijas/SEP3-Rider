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
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new CustomerService.CustomerServiceClient(channel);
        
        try
        {
            await client.CreateCustomerAsync(new CreateCustomerRequest
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Password = dto.Password
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task UpdateEmail(CustomerUpdateDTO dto)
    {
        if (!string.IsNullOrEmpty(dto.Email) && dto.Email.Length > 100)
            throw new InvalidDataException("Email cannot be longer than 100 characters.");
        
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new CustomerService.CustomerServiceClient(channel);

        try
        {
            await client.UpdateEmailAsync(new UpdateEmailRequest
            {
                AccountId = dto.AccountId,
                Email = dto.Email
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }

    }

    public async Task UpdatePassword(CustomerUpdateDTO dto)
    {
        if (!string.IsNullOrEmpty(dto.Password) && (dto.Password.Length > 16 || dto.Password.Length < 8))
            throw new InvalidDataException("Password must have between 8 and 16 characters.");
        
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new CustomerService.CustomerServiceClient(channel);

        try
        {
            await client.UpdatePasswordAsync(new UpdatePasswordRequest
            {
                AccountId = dto.AccountId,
                Password = dto.Password
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task UpdatePhoneNumber(CustomerUpdateDTO dto)
    {
        if (!string.IsNullOrEmpty(dto.PhoneNumber) && dto.PhoneNumber.Length > 20)
            throw new InvalidDataException("Phone number cannot be longer than 20 characters.");
        
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new CustomerService.CustomerServiceClient(channel);

        try
        {
            await client.UpdatePhoneNumberAsync(new UpdatePhoneNumberRequest
            {
                AccountId = dto.AccountId,
                PhoneNumber = dto.PhoneNumber
            });    
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }
}