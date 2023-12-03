using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class FoodSellersController : ControllerBase
{
    private readonly IFoodSellerLogic foodSellerLogic;

    public FoodSellersController(IFoodSellerLogic foodSellerLogic)
    {
        this.foodSellerLogic = foodSellerLogic;
    }
    
    [HttpPost, AllowAnonymous]
    public async Task<ActionResult> CreateAsync(FoodSellerCreationDTO dto)
    {
        try
        {
            await foodSellerLogic.CreateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch, Authorize(Policy = "MustBeFoodSeller")]
    public async Task<ActionResult> UpdateAsync(FoodSellerUpdateDTO dto)
    {
        try
        {
            if (!string.IsNullOrEmpty(dto.Name))
            {
                await foodSellerLogic.UpdateName(dto);
            }
            
            if (!string.IsNullOrEmpty(dto.Street) && dto.Number > 0 && !string.IsNullOrEmpty(dto.City))
            {
                await foodSellerLogic.UpdateAddress(dto);
            }
            
            if (!string.IsNullOrEmpty(dto.Email))
            {
                await foodSellerLogic.UpdateEmail(dto);
            }
            
            if (!string.IsNullOrEmpty(dto.Password))
            {
                await foodSellerLogic.UpdatePassword(dto);
            }
            
            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                await foodSellerLogic.UpdatePhoneNumber(dto);
            }
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{accountId:int}"), Authorize(Policy = "MustBeFoodSeller")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int accountId)
    {
        try
        {
            await foodSellerLogic.DeleteAccount(accountId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{accountId:int}")]
    public async Task<ActionResult<ReadFoodSellerDTO>> GetAsync([FromRoute] int accountId)
    {
        try
        { 
            var result  = await foodSellerLogic.GetFoodSellerById(accountId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet, Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult<List<ReadFoodSellerDTO>>> GetAllAsync()
    {
        try
        {
            var result = await foodSellerLogic.GetAllFoodSellers();
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}