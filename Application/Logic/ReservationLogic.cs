using System.Text.Json;
using Application.LogicInterfaces;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class ReservationLogic : IReservationLogic
{
    public async Task<IEnumerable<ReadCustomerReservationDTO>> ReadCustomerReservations(int customerId)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new ReservationService.ReservationServiceClient(channel);
        
        try
        {
            ReadReservationsListResponse response = await client.readCustomerReservationsAsync(new ReadCustomerReservationsRequest
            {
                CustomerId = customerId
            });
            
            string Json = response.List;
            Console.WriteLine(Json);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
                
            IEnumerable<ReadCustomerReservationDTO> reservations = JsonSerializer.Deserialize<IEnumerable<ReadCustomerReservationDTO>>(Json, options);

            return reservations;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<IEnumerable<ReadFoodSellerReservationDTO>> ReadFoodSellerReservations(int foodSellerId)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new ReservationService.ReservationServiceClient(channel);
        
        try
        {
            ReadReservationsListResponse response = await client.readFoodSellerReservationsAsync(new ReadFoodSellerReservationsRequest
            {
                FoodSellerId = foodSellerId
            });
            
            string Json = response.List; 
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            
            IEnumerable<ReadFoodSellerReservationDTO> reservations = JsonSerializer.Deserialize<IEnumerable<ReadFoodSellerReservationDTO>>(Json, options);
            return reservations;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }
    
    public async Task CreateAsync(ReservationCreationDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new ReservationService.ReservationServiceClient(channel);
        
        try
        {
            await client.createReservationAsync(new CreateReservationRequest
            {
                CustomerId = dto.CustomerId,
                FoodOfferId = dto.FoodOfferId
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task DeleteAsync(int reservationNumber)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new ReservationService.ReservationServiceClient(channel);
        
        try
        {
            await client.deleteReservationAsync(new DeleteReservationRequest
            {
                ReservationNumber = reservationNumber
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task CompleteReservationAsync(int reservationNumber)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new ReservationService.ReservationServiceClient(channel);
        
        try
        {
            await client.completeReservationAsync(new CompleteReservationRequest
            {
                ReservationNumber = reservationNumber
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }
}