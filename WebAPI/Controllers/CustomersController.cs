using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]

public class CustomersController: ControllerBase
{
    private readonly ICustomerLogic customerLogic;

    public CustomersController(ICustomerLogic customerLogic)
    {
        this.customerLogic = customerLogic;
    }

    [HttpPost]
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
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync(CustomerUpdateDTO dto)
    {
        try
        {
            if (!string.IsNullOrEmpty(dto.Password))
            {
                await customerLogic.UpdatePassword(dto);
            }

            if (!string.IsNullOrEmpty(dto.Email))
            {
                await customerLogic.UpdateEmail(dto);
            }
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}