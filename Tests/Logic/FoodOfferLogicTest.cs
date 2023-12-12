using Application.Logic;
using Domain;
using Domain.DTOs;
using Domain.Models;

namespace Tests.Logic;

public class FoodOfferLogicTest
{
    private FoodOfferLogic _logic;
    
    [SetUp]
    public void Setup()
    {
        _logic = new FoodOfferLogic(new GRPCService());
    }

    [Test]
    public async Task creating_food_offer_adds_it_to_the_database()
    {
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        FoodOfferCreationDTO dto = new FoodOfferCreationDTO(35, "Title", "Description", 100, start, end, "");
        Assert.DoesNotThrowAsync(() => _logic.CreateAsync(dto));
        FoodOffer foodOffer = await _logic.ReadFoodOfferById(16);
        Assert.That(foodOffer.Id, Is.EqualTo(16));
        Assert.That(foodOffer.Title, Is.EqualTo("Title"));
        Assert.That(foodOffer.Description, Is.EqualTo("Description"));
        Assert.That(foodOffer.FoodSeller, Is.EqualTo(foodSeller));
        Assert.That(foodOffer.StartPickupTime, Is.EqualTo(start));
        Assert.That(foodOffer.EndPickupTime, Is.EqualTo(end));
        Assert.False(foodOffer.IsReserved);
        Assert.False(foodOffer.IsCompleted);
    }

    [Test] public async Task reading_available_food_offers_returns_all_available_food_offers()
    {
        List<FoodOffer> foodOffers = await _logic.ReadAvailableFoodOffers();
        Assert.True(foodOffers.Count > 0);
        foreach (var var in foodOffers)
        {
            Assert.False(var.IsReserved);
            Assert.False(var.IsCompleted);
        }
    }

    [Test]
    public async Task reading_food_offers_by_food_seller_id_returns_offers_that_belong_only_to_specific_food_seller()
    {
        List<FoodOffer> foodOffers = await _logic.ReadFoodOffersByFoodSellerId(35);
        Assert.True(foodOffers.Count > 0);
        foreach (var var in foodOffers)
        {
            Assert.That(var.FoodSeller.AccountId, Is.EqualTo(35));
        }
    }

    [Test]
    public async Task reading_specific_food_offer_returns_all_its_information()
    {
        MyDate start = new MyDate(2023, 12, 11, 10, 00);
        MyDate end = new MyDate(2023, 12, 12, 10, 00);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        FoodOffer foodOffer = await _logic.ReadFoodOfferById(12);
        Assert.That(foodOffer.Id, Is.EqualTo(12));
        Assert.That(foodOffer.Title, Is.EqualTo("Title"));
        Assert.That(foodOffer.Description, Is.EqualTo("Description"));
        Assert.That(foodOffer.Price, Is.EqualTo(100));
        Assert.That(foodOffer.FoodSeller, Is.EqualTo(foodSeller));
        Assert.That(foodOffer.StartPickupTime, Is.EqualTo(start));
        Assert.That(foodOffer.EndPickupTime, Is.EqualTo(end));
        Assert.False(foodOffer.IsReserved);
        Assert.False(foodOffer.IsCompleted);
    }

    [Test]
    public async Task updating_food_offer_changes_its_data()
    {
        MyDate start = new MyDate(2023, 12, 12, 11, 00);
        MyDate end = new MyDate(2023, 12, 13, 13, 00);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "Name", "Street 10, Horsens");
        FoodOffer offer = new FoodOffer(12, foodSeller, "NewTitle", "NewDescription", 120, start, end, false, false);
        Assert.DoesNotThrowAsync(() => _logic.UpdateFoodOffer(offer));
        FoodOffer foodOffer = await _logic.ReadFoodOfferById(12);
        Assert.That(foodOffer.Id, Is.EqualTo(12));
        Assert.That(foodOffer.Title, Is.EqualTo("NewTitle"));
        Assert.That(foodOffer.Description, Is.EqualTo("NewDescription"));
        Assert.That(foodOffer.Price, Is.EqualTo(120));
        Assert.That(foodOffer.FoodSeller, Is.EqualTo(foodSeller));
        Assert.That(foodOffer.StartPickupTime, Is.EqualTo(start));
        Assert.That(foodOffer.EndPickupTime, Is.EqualTo(end));
        Assert.False(foodOffer.IsReserved);
        Assert.False(foodOffer.IsCompleted);
    }

    [Test]
    public async Task deleting_food_offer_removes_it_from_the_database()
    {
        Assert.DoesNotThrowAsync(() => _logic.DeleteFoodOffer(12));
        Assert.ThrowsAsync<Exception>(() => _logic.ReadFoodOfferById(12));
    }
}