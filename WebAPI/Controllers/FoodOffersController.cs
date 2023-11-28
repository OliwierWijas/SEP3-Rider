using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class FoodOffersController: ControllerBase
{
    private readonly IFoodOfferLogic foodOfferLogic;

    public FoodOffersController(IFoodOfferLogic foodOfferLogic)
    {
        this.foodOfferLogic = foodOfferLogic;
    }
    
    [HttpPost, Authorize(Policy = "MustBeFoodSeller")]
    public async Task<ActionResult> CreateAsync(FoodOfferCreationDTO dto)
    {
        try
        {
            await foodOfferLogic.CreateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch, Authorize(Policy = "MustBeFoodSeller")]
    public async Task<ActionResult> UpdateAsync(FoodOffer foodOffer)
    {
        try
        {
            await foodOfferLogic.UpdateFoodOffer(foodOffer);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{foodOfferId:int}"), Authorize(Policy = "MustBeFoodSeller")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int foodOfferId)
    {
        try
        {
            await foodOfferLogic.DeleteFoodOffer(foodOfferId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<FoodOffer>>> GetAvailableOffersAsync()
    {
        try
        {
            var result = await foodOfferLogic.ReadAvailableFoodOffers();
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{foodSellerId:int}")]
    public async Task<ActionResult<List<FoodOffer>>> GetFoodOffersByFoodSellerIdAsync([FromRoute] int foodSellerId)
    {
        try
        {
            var result = await foodOfferLogic.ReadFoodOffersByFoodSellerId(foodSellerId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Route("FoodOffer/{id:int}"), Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult<ReadFoodOffersDTO>> GetFoodOfferByIdAsync([FromRoute] int id)
    {
        try
        {
            var result = await foodOfferLogic.ReadFoodOfferById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    

}