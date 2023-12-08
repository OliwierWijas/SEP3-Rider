using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Authorize]
public class RatingsAndCommentsController : ControllerBase
{
    private readonly IRatingAndCommentLogic logic;

    public RatingsAndCommentsController(IRatingAndCommentLogic logic)
    {
        this.logic = logic;
    }
    
    [HttpPost, Route("Ratings"), Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult> CreateRatingAsync(RatingBasicDTO dto)
    {
        try
        {
            await logic.CreateRating(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost, Route("Comments"), Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult> CreateCommentAsync(CommentBasicDTO dto)
    {
        try
        {
            await logic.CreateComment(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }
    [HttpPatch, Route("Ratings"), Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult> UpdateRatingAsync(RatingBasicDTO dto)
    {
        try
        {
            await logic.UpdateRating(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete, Route("Comments/{commentId:int}"), Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult> DeleteCommentAsync([FromRoute] int commentId)
    {
        try
        {
            await logic.DeleteComment(commentId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }
    
    [HttpGet, Route("Comments/{foodSellerId:int}")]
    public async Task<ActionResult<List<Comment>>> GetCommentsByFoodSellerIdAsync([FromRoute]int foodSellerId)
    {
        try
        {
            var result = await logic.ReadCommentsByFoodSellerId(foodSellerId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("Ratings/{foodSellerId:int}")]
    public async Task<ActionResult<double>> GetAverageRatingByFoodSellerIdAsync([FromRoute]int foodSellerId)
    {
        try
        {
            var result = await logic.ReadAverageRatingByFoodSellerId(foodSellerId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPost, Route("ReadRatings")]
    public async Task<ActionResult<int>> GetRatingAsync([FromBody] ReadRatingDTO dto)
    {
        try
        {
            var result = await logic.ReadRating(dto);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}