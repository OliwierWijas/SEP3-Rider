using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface ICustomerLogic
{
    Task CreateAsync(CustomerCreationDTO customerDTO);
    
    
}