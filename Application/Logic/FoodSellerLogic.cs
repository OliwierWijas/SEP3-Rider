using System.Text.Json;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class FoodSellerLogic : IFoodSellerLogic
{
    private  FoodSellerService.FoodSellerServiceClient client;
    public FoodSellerLogic(GRPCService service)
    {
        GrpcChannel channel = service.Channel;
        client = new  FoodSellerService.FoodSellerServiceClient(channel);
    }
    public async Task CreateAsync(FoodSellerCreationDTO dto)
    {
      
        try
        {
            await client.CreateFoodSellerAsync(new CreateFoodSellerRequest()
            {
                Name = dto.Name,
                Street = dto.Street,
                Number = dto.Number,
                City = dto.City,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Password = dto.Password,
                Photo = dto.Photo
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
        if (dto.Street.Length > 100)
            throw new InvalidDataException("Street cannot be longer than 100 characters.");
        if (dto.City.Length > 100)
            throw new InvalidDataException("City cannot be longer than 100 characters.");
        
        try
        {
            await client.UpdateAddressAsync(new UpdateFoodSellerAddressRequest()
            {
                AccountId = dto.AccountId,
                Street = dto.Street,
                Number = dto.Number,
                City = dto.City
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

    public async Task<FoodSeller> GetFoodSellerById(int accountId)
    {
        try
        {
             GetFoodSellerByIdResponse response = await client.GetFoodSellerByIdAsync(new GetFoodSellerByIdRequest
            {
                AccountId = accountId
            });
             FoodSeller seller = new FoodSeller(response.AccountId, response.Email, response.PhoneNumber,
                 response.Name, response.Address);
             return seller;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
        
    }

    public async Task<List<FoodSeller>> GetAllFoodSellers()
    {
        try
        {
            GetAllFoodSellersResponse response = await client.GetAllFoodSellersAsync(new GetAllFoodSellersRequest());
            string Json = response.List;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            List<FoodSeller> foodSellers = JsonSerializer.Deserialize<List<FoodSeller>>(Json, options)!;

            return foodSellers;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<string> GetFoodSellerPhoto(int id)
    {
        try
        {
            GetPhotoResponse response = await client.GetFoodSellerPhotoAsync(new GetPhotoRequest()
            {
                AccountId = id
            });
            return response.Photo;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }
}