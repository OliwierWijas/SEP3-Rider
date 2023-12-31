﻿using System.Security.AccessControl;
using System.Text.Json;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
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
    public async Task<IEnumerable<Reservation>> ReadCustomerReservations(int customerId)
    {
        try
        {
            ReadReservationsListResponse response = await client.readCustomerReservationsAsync(new ReadCustomerReservationsRequest
            {
                CustomerId = customerId
            });
            
            string Json = response.List;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
                
            IEnumerable<Reservation> reservations = JsonSerializer.Deserialize<IEnumerable<Reservation>>(Json, options)!;

            return reservations;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<IEnumerable<Reservation>> ReadFoodSellerReservations(int foodSellerId)
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
            
            IEnumerable<Reservation> reservations = JsonSerializer.Deserialize<IEnumerable<Reservation>>(Json, options)!;
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
            Console.WriteLine(e.Message);
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