using Application.Logic;
using Domain;
using Domain.DTOs;
using Domain.Models;

namespace Tests.Logic;

public class RatingAndCommentLogicTest
{
    private RatingAndCommentLogic _logic;
        
    [SetUp]
    public void Setup()
    {
        _logic = new RatingAndCommentLogic(new GRPCService());
    }

    [Test]
    public async Task creating_rating_adds_it_to_the_database()
    {
        RatingBasicDTO dto = new RatingBasicDTO(35, 10, 4);
        Assert.DoesNotThrowAsync(() => _logic.CreateRating(dto));
        ReadRatingDTO readRatingDto = new ReadRatingDTO(10, 35);
        int rate = await _logic.ReadRating(readRatingDto);
        Assert.AreEqual(rate, 4);
    }

    [Test]
    public async Task changing_rating_updates_the_database()
    {
        RatingBasicDTO dto = new RatingBasicDTO(35, 10, 5);
        Assert.DoesNotThrowAsync(() => _logic.CreateRating(dto));
        ReadRatingDTO readRatingDto = new ReadRatingDTO(10, 35);
        int rate = await _logic.ReadRating(readRatingDto);
        Assert.AreEqual(rate, 5);
    }

    [Test]
    public async Task creating_comment_adds_it_to_the_database()
    {
        CommentBasicDTO dto = new CommentBasicDTO(35, 10, "comment1");
        Assert.DoesNotThrowAsync(() => _logic.CreateComment(dto));
        List<Comment> comments = await _logic.ReadCommentsByFoodSellerId(35);
        Customer customer = new Customer("firstName", "lastName", "", "", null);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "", "");
        MyDate date = new MyDate(2023, 12, 12, 0, 0);
        Comment comment = new Comment(9, customer, foodSeller, date, "comment1");
        Assert.True(comments.Contains(comment));
    }

    [Test]
    public async Task reading_specific_rating_made_by_customer_returns_correct_value()
    {
        ReadRatingDTO readRatingDto = new ReadRatingDTO(10, 35);
        int rate = await _logic.ReadRating(readRatingDto);
        Assert.AreEqual(rate, 5);
    }

    [Test]
    public async Task reading_average_rating_returns_correct_value()
    {
        double rating = await _logic.ReadAverageRatingByFoodSellerId(2);
        Assert.AreEqual(rating, 3);
    }

    [Test]
    public async Task reading_comments_of_food_seller_returns_correct_data()
    {
        List<Comment> comments = await _logic.ReadCommentsByFoodSellerId(35);
        Customer customer = new Customer("firstName", "lastName", "", "", null);
        FoodSeller foodSeller = new FoodSeller(35, "", "", "", "");
        MyDate date = new MyDate(2023, 12, 12, 0, 0);
        Comment comment = new Comment(9, customer, foodSeller, date, "comment1");
        Assert.True(comments.Contains(comment));
    }
}