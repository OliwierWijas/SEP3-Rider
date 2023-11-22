using Domain;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IFoodOfferService
{
    Task CreateAsync(FoodOfferCreationDTO dto);
    Task UpdateAsync(FoodOffer foodOffer);
    Task DeleteAsync(int foodOfferId);
    Task<List<FoodOffer>> GetAvailableFoodOffersAsync();
    Task<List<FoodOffer>> GetFoodOffersByFoodSellerIdAsync(int foodSellerId);
}