using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRatingAndCommentLogic
{
    
    Task CreateRating(RatingBasicDTO dto);
    Task CreateComment(CommentBasicDTO dto);
    Task UpdateRating(RatingBasicDTO dto);
    Task DeleteComment(int commentId);
    Task<List<Comment>> ReadCommentsByFoodSellerId(int foodSellerId);
    Task<double> ReadAverageRatingByFoodSellerId(int foodSellerId);
    Task<int> ReadRating(ReadRatingDTO dto);
}