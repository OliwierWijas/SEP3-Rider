using Domain;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IFoodOfferLogic
{
    Task CreateAsync(FoodOfferCreationDTO dto);
    Task<List<FoodOffer>> ReadAvailableFoodOffers();
    Task<List<FoodOffer>> ReadFoodOffersByFoodSellerId(int foodSellerId);
    Task UpdateFoodOffer(FoodOffer foodOffer);
    Task DeleteFoodOffer(int foodOfferId);
    Task<FoodOffer> ReadFoodOfferById(int id);
}