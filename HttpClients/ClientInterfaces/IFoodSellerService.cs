using Domain;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IFoodSellerService
{
    Task CreateAsync(FoodSellerCreationDTO dto);
    Task UpdateAsync(FoodSellerUpdateDTO dto);
    Task DeleteAsync(int accountId);
    Task<FoodSeller> GetAsync(int accountId);
    Task<List<FoodSeller>> GetAllAsync();
    Task<string> GetPhotoAsync(int accountId);
}