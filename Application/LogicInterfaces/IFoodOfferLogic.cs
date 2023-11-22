using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IFoodOfferLogic
{
    Task CreateAsync(FoodOfferCreationDTO dto);
    Task<List<ReadFoodOffersDTO>> ReadAvailableFoodOffers();
    Task<List<ReadFoodOffersDTO>> ReadFoodOffersByFoodSellerId(int foodSellerId);
    Task UpdateFoodOffer(FoodOffer foodOffer);
    Task DeleteFoodOffer(int foodOfferId);
}