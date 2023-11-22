using Application.LogicInterfaces;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class FoodSellerLogic : IFoodSellerLogic
{
    public async Task CreateAsync(FoodSellerCreationDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodSellerService.FoodSellerServiceClient(channel);
        
        try
        {
            await client.CreateFoodSellerAsync(new CreateFoodSellerRequest()
            {
                Name = dto.Name,
                Address = dto.Address,
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

    public async Task UpdateName(FoodSellerUpdateDTO dto)
    {
        if (dto.Name.Length > 100)
            throw new InvalidDataException("Name cannot be longer than 100 characters.");
        
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodSellerService.FoodSellerServiceClient(channel);

        try
        {
            await client.UpdateNameAsync(new UpdateFoodSellerNameRequest()
            {
                AccountId = dto.AccountId,
                Name = dto.Name
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }    
    }

    public async Task UpdateAddress(FoodSellerUpdateDTO dto)
    {
        if (dto.Address.Length > 200)
            throw new InvalidDataException("Address cannot be longer than 200 characters.");
        
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodSellerService.FoodSellerServiceClient(channel);

        try
        {
            await client.UpdateAddressAsync(new UpdateFoodSellerAddressRequest()
            {
                AccountId = dto.AccountId,
                Address = dto.Address
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }    
    }

    public async Task UpdateEmail(FoodSellerUpdateDTO dto)
    {
        if (dto.Email.Length > 100)
            throw new InvalidDataException("Email cannot be longer than 200 characters.");
        
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodSellerService.FoodSellerServiceClient(channel);

        try
        {
            await client.UpdateEmailAsync(new UpdateFoodSellerEmailRequest
            {
                AccountId = dto.AccountId,
                Email = dto.Email
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }    
    }

    public async Task UpdatePassword(FoodSellerUpdateDTO dto)
    {
        if (dto.Password.Length > 16 || dto.Password.Length < 8)
            throw new InvalidDataException("Password must have between 8 and 16 characters.");
        
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodSellerService.FoodSellerServiceClient(channel);

        try
        {
            await client.UpdatePasswordAsync(new UpdateFoodSellerPasswordRequest
            {
                AccountId = dto.AccountId,
                Password = dto.Password
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }    
    }

    public async Task UpdatePhoneNumber(FoodSellerUpdateDTO dto)
    {
        if (dto.PhoneNumber.Length > 20)
            throw new InvalidDataException("Phone number cannot be longer than 20 characters.");
        
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodSellerService.FoodSellerServiceClient(channel);

        try
        {
            await client.UpdatePhoneNumberAsync(new UpdateFoodSellerPhoneNumberRequest
            {
                AccountId = dto.AccountId,
                PhoneNumber = dto.PhoneNumber
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        } 
    }

    public async Task DeleteAccount(int accountId)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodSellerService.FoodSellerServiceClient(channel);
        
        try
        {
            await client.DeleteAccountAsync(new DeleteFoodSellerAccountRequest
            {
                AccountId = accountId
            });    
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }     
    }
    
    
}