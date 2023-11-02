using Domain;
using Domain.DTOs;
using GrpcClient;

namespace Application.LogicInterfaces;

public interface ICustomerLogic
{
    Task CreateAsync(CustomerCreationDTO dto);
    Task UpdateEmail(CustomerUpdateDTO dto);
    Task UpdatePassword(CustomerUpdateDTO dto);
    Task UpdatePhoneNumber(CustomerUpdateDTO dto);
}