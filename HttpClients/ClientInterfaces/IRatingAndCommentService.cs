using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IRatingAndCommentService
{
    Task CreateRatingAsync(RatingBasicDTO dto);
    Task CreateCommentAsync(CommentBasicDTO dto);
    Task UpdateRatingAsync(RatingBasicDTO dto);
    Task DeleteCommentAsync(int commentId);
    Task<List<Comment>> ReadCommentsByFoodSellerIdAsync(int foodSellerId);
    Task<double> ReadAverageRatingByFoodSellerIdAsync(int foodSellerId);
    Task<int> ReadRatingAsync(ReadRatingDTO dto);
}