using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
public class ReservationsController : ControllerBase
{
    private readonly IReservationLogic reservationLogic;

    public ReservationsController(IReservationLogic reservationLogic)
    {
        this.reservationLogic = reservationLogic;
    }

    [HttpPost, Route("Reservations"), Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult> CreateAsync(ReservationCreationDTO dto)
    {
        try
        {
            await reservationLogic.CreateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{reservationNumber:int}"), Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int reservationNumber)
    {

        try
        {
            await reservationLogic.DeleteAsync(reservationNumber);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("CustomerReservations/{customerId:int}"), Authorize(Policy = "MustBeCustomer")]
    public async Task<ActionResult<IEnumerable<ReadCustomerReservationDTO>>> GetCustomerReservationsAsync([FromRoute]int customerId)
    {
        try
        {
            var result = await reservationLogic.ReadCustomerReservations(customerId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("FoodSellerReservations/{foodSellerId:int}"), Authorize(Policy = "MustBeFoodSeller")]
    public async Task<ActionResult<IEnumerable<ReadFoodSellerReservationDTO>>> GetFoodSellerReservationsAsync([FromRoute]int foodSellerId)
    {
        try
        {
            var result = await reservationLogic.ReadFoodSellerReservations(foodSellerId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch, Route("FoodSellerReservations"), Authorize(Policy = "MustBeFoodSeller")]
    public async Task<ActionResult> CompleteReservationAsync(int reservationNumber)
    {
        try
        {
            await reservationLogic.CompleteReservationAsync(reservationNumber);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}