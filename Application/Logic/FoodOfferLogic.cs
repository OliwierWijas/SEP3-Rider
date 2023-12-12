using System.Text.Json;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Models;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class FoodOfferLogic: IFoodOfferLogic
{
    private FoodOfferService.FoodOfferServiceClient client;
    public FoodOfferLogic(GRPCService service)
    {
        GrpcChannel channel = service.Channel;
        client = new FoodOfferService.FoodOfferServiceClient(channel);
    }
    public async Task CreateAsync(FoodOfferCreationDTO dto)
    {
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
                }),
                Photo = dto.Photo
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<List<FoodOffer>> ReadAvailableFoodOffers()
    {
        try
        {
            ReadFoodOffersListResponse response = await client.readAvailableFoodOffersAsync(new ReadAvailableFoodOffersRequest());
            string Json = response.List;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            List<FoodOffer> foodOffers = JsonSerializer.Deserialize<List<FoodOffer>>(Json, options);

            return foodOffers;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<List<FoodOffer>> ReadFoodOffersByFoodSellerId(int foodSellerId)
    {
        try
        {
            ReadFoodOffersListResponse response = await client.readFoodOffersByFoodSellerIdAsync(new ReadFoodOffersByFoodSellerIdRequest
            {
                FoodSellerId = foodSellerId
            });
            
            string Json = response.List;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            
            List<FoodOffer> foodOffers = JsonSerializer.Deserialize<List<FoodOffer>>(Json, options);
            
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
        try
        {
            await client.updateFoodOfferAsync(new UpdateFoodOfferRequest
            {
                
                FoodOfferId = foodOffer.Id,
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

    public async Task<FoodOffer> ReadFoodOfferById(int id)
    {
        try
        {
            ReadFoodOfferByIdResponse response = await client.readFoodOfferByIdAsync(new ReadFoodOfferByIdRequest
            {
                Id = id
            });
            
            string JsonStartTime = response.StartPickUpTime;
            string JsonEndTime = response.EndPickUpTime;
            string JsonFoodSeller = response.FoodSeller;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            MyDate? start = JsonSerializer.Deserialize<MyDate>(JsonStartTime, options);
            MyDate? end = JsonSerializer.Deserialize<MyDate>(JsonEndTime, options);
            FoodSeller? foodSeller = JsonSerializer.Deserialize<FoodSeller>(JsonFoodSeller, options);
            FoodOffer foodOffer = new FoodOffer(response.Id, foodSeller, response.Title, response.Description,
                response.Price, start, end, response.IsReserved,
                response.IsCompleted);
            foodOffer.Photo = response.Photo;
            return foodOffer;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }
}