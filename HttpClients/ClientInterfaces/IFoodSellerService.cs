using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IFoodSellerService
{
    Task CreateAsync(FoodSellerCreationDTO dto);
    Task UpdateAsync(FoodSellerUpdateDTO dto);
    Task DeleteAsync(int accountId);
    Task<ReadFoodSellerDTO> GetAsync(int accountId);
}