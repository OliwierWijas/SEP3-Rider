using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IFoodSellerLogic
{
    Task CreateAsync(FoodSellerCreationDTO dto);
    Task UpdateName(FoodSellerUpdateDTO dto);
    Task UpdateAddress(FoodSellerUpdateDTO dto);
    Task UpdateEmail(FoodSellerUpdateDTO dto);
    Task UpdatePassword(FoodSellerUpdateDTO dto);
    Task UpdatePhoneNumber(FoodSellerUpdateDTO dto);
    Task DeleteAccount(int accountId);
    Task<ReadFoodSellerDTO> GetFoodSellerById(int accountId);
    Task<List<ReadFoodSellerDTO>> GetAllFoodSellers();
}