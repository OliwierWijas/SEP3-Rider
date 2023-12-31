﻿using System.Text.Json;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Models;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class RatingAndCommentLogic : IRatingAndCommentLogic
{   
    private  RatingAndCommentService.RatingAndCommentServiceClient client;
    public RatingAndCommentLogic(GRPCService service)
    {
        GrpcChannel channel = service.Channel;
        client = new RatingAndCommentService.RatingAndCommentServiceClient(channel);
    }
    public async Task CreateRating(RatingBasicDTO dto)
    {
        try
        {
            await client.createRatingAsync(new CreateAndUpdateRatingRequest
            {
                CustomerId = dto.CustomerId,
                FoodSellerId =dto.FoodSellerId,
                Rate = dto.Rate
            });
        }
        catch (Exception e)
        {
            if (e.Message.Contains("User already rated the Food Seller."))
            {
                try
                {
                    await UpdateRating(dto);
                }
                catch (Exception ex)
                {
                    string[] message1 = ex.Message.Split("\"");
                    throw new Exception(message1[3]);
                }
                
            }
            else
            {
                string[] message = e.Message.Split("\"");
                throw new Exception(message[3]);
            } 
        }
    }

    public async Task CreateComment(CommentBasicDTO dto)
    {
        try
        {
            await client.createCommentAsync(new CreateCommentRequest
            {
                CustomerId = dto.CustomerId,
                FoodSellerId =dto.FoodSellerId,
                Comment = dto.Comment
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task UpdateRating(RatingBasicDTO dto)
    {
        try
        {
            await client.updateRatingAsync(new CreateAndUpdateRatingRequest
            {
                CustomerId = dto.CustomerId,
                FoodSellerId =dto.FoodSellerId,
                Rate = dto.Rate
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task DeleteComment(int commentId)
    {
        try
        {
            await client.deleteCommentAsync(new DeleteCommentRequest
            {
                CommentId = commentId
            });
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<List<Comment>> ReadCommentsByFoodSellerId(int foodSellerId)
    {
        try
        {
            ReadCommentsByFoodSellerIdResponse response = await client.readCommentsByFoodSellerIdAsync(new ReadCommentsByFoodSellerIdRequest
            {
                FoodSellerId = foodSellerId
            });
            
            string Json = response.List;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
            
            List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(Json, options)!;

            return comments;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }

    public async Task<double> ReadAverageRatingByFoodSellerId(int foodSellerId)
    {
        try
        {
            ReadAverageRatingByFoodSellerIdResponse response = await client.readAverageRatingByFoodSellerIdAsync(new ReadAverageRatingByFoodSellerIdRequest
            {
                FoodSellerId = foodSellerId
            });

            return response.Rate;
        }
        catch (Exception e)
        {
            string[] message = e.Message.Split("\"");
            throw new Exception(message[3]);
        }
    }
}