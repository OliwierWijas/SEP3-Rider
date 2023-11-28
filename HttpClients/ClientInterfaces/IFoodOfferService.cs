using Domain;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IFoodOfferService
{
    Task CreateAsync(FoodOfferCreationDTO dto);
    Task UpdateAsync(FoodOffer foodOffer);
    Task DeleteAsync(int foodOfferId);
    Task<List<ReadFoodOffersDTO>> GetAvailableFoodOffersAsync();
    Task<List<ReadFoodOffersDTO>> GetFoodOffersByFoodSellerIdAsync(int foodSellerId);
    Task<FoodOffer> GetFoodOfferAsync(int foodSellerId, int foodOfferId);
    Task<ReadFoodOffersDTO> GetFoodOfferByIdAsync(int id);
}