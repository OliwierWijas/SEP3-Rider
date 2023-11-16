using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class CustomersController: ControllerBase
{
    private readonly ICustomerLogic customerLogic;

    public CustomersController(ICustomerLogic customerLogic)
    {
        this.customerLogic = customerLogic;
    }

    [HttpPost, AllowAnonymous]
    public async Task<ActionResult> CreateAsync(CustomerCreationDTO dto)
    {
        try
        {
            await customerLogic.CreateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPatch, Authorize("MustBeCustomer")]
    public async Task<ActionResult> UpdateAsync(CustomerUpdateDTO dto)
    {
        try
        {
            if (!string.IsNullOrEmpty(dto.Email))
            {
                await customerLogic.UpdateEmail(dto);
            }
            
            if (!string.IsNullOrEmpty(dto.Password))
            {
                await customerLogic.UpdatePassword(dto);
            }
            
            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                await customerLogic.UpdatePhoneNumber(dto);
            }
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{accountId:int}"), Authorize("MustBeCustomer")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int accountId)
    {
        try
        {
            await customerLogic.DeleteAccount(accountId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}