using System.Text.Json;
using Application.LogicInterfaces;
using Domain.DTOs;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

namespace Application.Logic;

public class RatingAndCommentLogic : IRatingAndCommentLogic
{
    public async Task CreateRating(RatingBasicDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new RatingAndCommentService.RatingAndCommentServiceClient(channel);
        
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
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new RatingAndCommentService.RatingAndCommentServiceClient(channel);
        
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
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new RatingAndCommentService.RatingAndCommentServiceClient(channel);
        
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
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new RatingAndCommentService.RatingAndCommentServiceClient(channel);
        
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

    public async Task<List<ReadCommentDTO>> ReadCommentsByFoodSellerId(int foodSellerId)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new RatingAndCommentService.RatingAndCommentServiceClient(channel);
        
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
            
            List<ReadCommentDTO> comments = JsonSerializer.Deserialize<List<ReadCommentDTO>>(Json, options)!;

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
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new RatingAndCommentService.RatingAndCommentServiceClient(channel);
        
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

    public async Task<int> ReadRating(ReadRatingDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:8080", new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        var client = new RatingAndCommentService.RatingAndCommentServiceClient(channel);
        
        try
        {
            ReadRatingResponse response = await client.readRatingAsync(new ReadRatingRequest
            {
                CustomerId = dto.CustomerId,
                FoodSellerId = dto.FoodSellerId
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