using System.Text.Json;
using Application.LogicInterfaces;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class ReservationLogic : IReservationLogic
{ 
    private   ReservationService.ReservationServiceClient client;
    public ReservationLogic(GRPCService service)
    {
        GrpcChannel channel = service.Channel;
        client = new  ReservationService.ReservationServiceClient(channel);
    }
    public async Task<IEnumerable<ReadCustomerReservationDTO>> ReadCustomerReservations(int customerId)
    {
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