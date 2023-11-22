using System.Text.Json;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class FoodOfferLogic: IFoodOfferLogic
{
    public async Task CreateAsync(FoodOfferCreationDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodOfferService.FoodOfferServiceClient(channel);
        
        try
        {
            await client.createFoodOfferAsync(new CreateFoodOfferRequest
            {
                FoodSellerId = dto.FoodSellerId,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                StartPickUpTime = JsonSerializer.Serialize(dto.StartPickupTime, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }),
                EndPickUpTime = JsonSerializer.Serialize(dto.EndPickupTime, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<List<ReadFoodOffersDTO>> ReadAvailableFoodOffers()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodOfferService.FoodOfferServiceClient(channel);
        
        try
        {
            ReadFoodOffersListResponse response = await client.readAvailableFoodOffersAsync(new ReadAvailableFoodOffersRequest());
            string Json = response.List;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            List<ReadFoodOffersDTO> foodOffers = JsonSerializer.Deserialize<List<ReadFoodOffersDTO>>(Json, options);

            return foodOffers;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<List<ReadFoodOffersDTO>> ReadFoodOffersByFoodSellerId(int foodSellerId)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodOfferService.FoodOfferServiceClient(channel);
        
        try
        {
            ReadFoodOffersListResponse response = await client.readFoodOffersByFoodSellerIdAsync(new ReadFoodOffersByFoodSellerIdRequest
            {
                FoodSellerId = foodSellerId
            });
            
            string Json = response.List;
            Console.WriteLine(Json);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            
            List<ReadFoodOffersDTO> foodOffers = JsonSerializer.Deserialize<List<ReadFoodOffersDTO>>(Json, options);

            foreach (var VARIABLE in foodOffers)
            {
                Console.WriteLine(VARIABLE.Title);
                Console.WriteLine(VARIABLE.StartPickupTime.ToString());
            }

            return foodOffers;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task UpdateFoodOffer(FoodOffer foodOffer)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodOfferService.FoodOfferServiceClient(channel);
        
        try
        {
            await client.updateFoodOfferAsync(new UpdateFoodOfferRequest
            {
                
                FoodOfferId = foodOffer.FoodOfferId,
                Title = foodOffer.Title,
                Description = foodOffer.Description,
                Price = foodOffer.Price,
                StartPickUpTime = JsonSerializer.Serialize(foodOffer.StartPickupTime),
                EndPickUpTime = JsonSerializer.Serialize(foodOffer.EndPickupTime)
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task DeleteFoodOffer(int foodOfferId)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new FoodOfferService.FoodOfferServiceClient(channel);
        
        try
        {
            await client.deleteFoodOfferAsync(new DeleteFoodOfferRequest
            {
                FoodOfferId = foodOfferId
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }
}