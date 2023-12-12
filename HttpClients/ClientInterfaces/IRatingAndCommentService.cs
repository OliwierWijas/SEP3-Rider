using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IRatingAndCommentService
{
    Task CreateRatingAsync(RatingBasicDTO dto);
    Task CreateCommentAsync(CommentBasicDTO dto);
    Task<List<Comment>> ReadCommentsByFoodSellerIdAsync(int foodSellerId);
    Task<double> ReadAverageRatingByFoodSellerIdAsync(int foodSellerId);
}