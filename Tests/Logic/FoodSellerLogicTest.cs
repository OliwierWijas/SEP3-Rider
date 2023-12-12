using Application.Logic;
using Domain;
using Domain.DTOs;

namespace Tests.Logic;

public class FoodSellerLogicTest
{
    private FoodSellerLogic _logic;
        
    [SetUp]
    public void Setup()
    {
        _logic = new FoodSellerLogic(new GRPCService());
    }
    
    [Test]
    public async Task creating_food_seller_makes_an_account()
    {
        FoodSellerCreationDTO dto = new FoodSellerCreationDTO("Name", "Street", 10, "Horsens", "FoodSeller@via.dk", "123455432", "Password123", "");
        await _logic.CreateAsync(dto);
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "FoodSeller@via.dk",
            Password = "Password123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(35, Is.EqualTo(user.Id));
        Assert.That(user.Email, Is.EqualTo("FoodSeller@via.dk"));
        Assert.That(user.Password, Is.EqualTo("Password123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("123455432"));
        Assert.That(user.Address, Is.EqualTo("Street 10, Horsens"));
        Assert.That(user.Name, Is.EqualTo("Name"));
        Assert.That(user.FirstName, Is.EqualTo(""));
        Assert.That(user.LastName, Is.EqualTo(""));
        Assert.That(user.Type, Is.EqualTo("foodseller"));
    }

    [Test]
    public async Task creating_food_seller_with_existing_email_throws_an_exception()
    {
        FoodSellerCreationDTO dto = new FoodSellerCreationDTO("Name", "Street", 10, "Horsens", "FoodSeller@via.dk", "123455432", "Password123", "");
        Assert.CatchAsync<Exception>(() => _logic.CreateAsync(dto));
    }
    
    [Test]
    public async Task creating_food_seller_with_existing_phone_number_throws_an_exception()
    {
        FoodSellerCreationDTO dto = new FoodSellerCreationDTO("Name", "Street", 10, "Horsens", "FoodSeller1@via.dk", "123455432", "Password123", "");
        Assert.CatchAsync<Exception>(() => _logic.CreateAsync(dto));
    }

    [Test]
    public async Task updating_email_changes_data()
    {
        FoodSellerUpdateDTO update = new FoodSellerUpdateDTO(29, null, null, 0, null, "FoodSeller1@via.dk", null, null);
        Assert.DoesNotThrowAsync(() => _logic.UpdateEmail(update));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "FoodSeller1@via.dk",
            Password = "Password123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(user.Id, Is.EqualTo(29));
        Assert.That(user.Email, Is.EqualTo("FoodSeller1@via.dk"));
        Assert.That(user.Password, Is.EqualTo("Password123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("123455432"));
        Assert.That(user.Address, Is.EqualTo("Street 10, Horsens"));
        Assert.That(user.Name, Is.EqualTo("Name"));
        Assert.That(user.FirstName, Is.EqualTo(""));
        Assert.That(user.LastName, Is.EqualTo(""));
        Assert.That(user.Type, Is.EqualTo("foodseller"));
    }
    
    [Test]
    public async Task updating_phone_number_changes_data()
    {
        FoodSellerUpdateDTO update = new FoodSellerUpdateDTO(29, null, null, 0, null, null, null, "334455667");
        Assert.DoesNotThrowAsync(() => _logic.UpdatePhoneNumber(update));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "FoodSeller1@via.dk",
            Password = "Password123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(user.Id, Is.EqualTo(29));
        Assert.That(user.Email, Is.EqualTo("FoodSeller1@via.dk"));
        Assert.That(user.Password, Is.EqualTo("Password123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("334455667"));
        Assert.That(user.Address, Is.EqualTo("Street 10, Horsens"));
        Assert.That(user.Name, Is.EqualTo("Name"));
        Assert.That(user.FirstName, Is.EqualTo(""));
        Assert.That(user.LastName, Is.EqualTo(""));
        Assert.That(user.Type, Is.EqualTo("foodseller"));
    }
    
    [Test]
    public async Task updating_password_changes_data()
    {
        FoodSellerUpdateDTO update = new FoodSellerUpdateDTO(29, null, null, 0, null, null, "NewPassword123", null);
        Assert.DoesNotThrowAsync(() => _logic.UpdatePassword(update));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "FoodSeller1@via.dk",
            Password = "NewPassword123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(user.Id, Is.EqualTo(29));
        Assert.That(user.Email, Is.EqualTo("FoodSeller1@via.dk"));
        Assert.That(user.Password, Is.EqualTo("NewPassword123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("334455667"));
        Assert.That(user.Address, Is.EqualTo("Street 10, Horsens"));
        Assert.That(user.Name, Is.EqualTo("Name"));
        Assert.That(user.FirstName, Is.EqualTo(""));
        Assert.That(user.LastName, Is.EqualTo(""));
        Assert.That(user.Type, Is.EqualTo("foodseller"));
    }

    [Test]
    public async Task updating_name_changes_data()
    {
        FoodSellerUpdateDTO update = new FoodSellerUpdateDTO(29, "NewName", null, 0, null, null, null, null);
        Assert.DoesNotThrowAsync(() => _logic.UpdateName(update));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "FoodSeller1@via.dk",
            Password = "NewPassword123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(user.Id, Is.EqualTo(29));
        Assert.That(user.Email, Is.EqualTo("FoodSeller1@via.dk"));
        Assert.That(user.Password, Is.EqualTo("NewPassword123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("334455667"));
        Assert.That(user.Address, Is.EqualTo("Street 10, Horsens"));
        Assert.That(user.Name, Is.EqualTo("NewName"));
        Assert.That(user.FirstName, Is.EqualTo(""));
        Assert.That(user.LastName, Is.EqualTo(""));
        Assert.That(user.Type, Is.EqualTo("foodseller"));
    }
    
    [Test]
    public async Task updating_address_changes_data()
    {
        FoodSellerUpdateDTO update = new FoodSellerUpdateDTO(29, null, "NewStreet", 2, "Copenhagen", null, null, null);
        Assert.DoesNotThrowAsync(() => _logic.UpdateAddress(update));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "FoodSeller1@via.dk",
            Password = "NewPassword123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(user.Id, Is.EqualTo(29));
        Assert.That(user.Email, Is.EqualTo("FoodSeller1@via.dk"));
        Assert.That(user.Password, Is.EqualTo("NewPassword123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("334455667"));
        Assert.That(user.Address, Is.EqualTo("NewStreet 2, Copenhagen"));
        Assert.That(user.Name, Is.EqualTo("NewName"));
        Assert.That(user.FirstName, Is.EqualTo(""));
        Assert.That(user.LastName, Is.EqualTo(""));
        Assert.That(user.Type, Is.EqualTo("foodseller"));
    }
    
        
    [Test]
    public async Task deleting_account_removes_data()
    {
        Assert.DoesNotThrowAsync(() => _logic.DeleteAccount(29));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "FoodSeller1@via.dk",
            Password = "NewPassword123"
        };
        Assert.CatchAsync<Exception>(() => loginLogic.LoginAsync(loginDto));
    }

    [Test]
    public async Task deleting_account_with_reserved_food_offers_throws_an_exception()
    {
        Assert.CatchAsync<Exception>(() => _logic.DeleteAccount(2));
    }

    [Test]
    public async Task getting_food_seller_by_id_returns_correct_data()
    {
        FoodSeller foodSeller = await _logic.GetFoodSellerById(29);
        Assert.That(foodSeller.AccountId, Is.EqualTo(29));
        Assert.That(foodSeller.Email, Is.EqualTo("FoodSeller1@via.dk"));
        Assert.That(foodSeller.PhoneNumber, Is.EqualTo("334455667"));
        Assert.That(foodSeller.Address, Is.EqualTo("NewStreet 2, Copenhagen"));
        Assert.That(foodSeller.Name, Is.EqualTo("NewName"));
    }

    [Test]
    public async Task getting_food_sellers_returns_correct_data()
    {
        List<FoodSeller> foodSellers = await _logic.GetAllFoodSellers();
        Assert.That(2, Is.EqualTo(foodSellers.Count));
        FoodSeller foodSeller1 = foodSellers[0];
        Assert.That(foodSeller1.AccountId, Is.EqualTo(2));
        Assert.That(foodSeller1.Email, Is.EqualTo("burger.concept@gmail.com"));
        Assert.That(foodSeller1.PhoneNumber, Is.EqualTo("0291375829"));
        Assert.That(foodSeller1.Address, Is.EqualTo("Main 70, Horsens"));
        Assert.That(foodSeller1.Name, Is.EqualTo("Burger Concept"));
        FoodSeller foodSeller2 = foodSellers[1];
        Assert.That(foodSeller2.AccountId, Is.EqualTo(29));
        Assert.That(foodSeller2.Email, Is.EqualTo("FoodSeller1@via.dk"));
        Assert.That(foodSeller2.PhoneNumber, Is.EqualTo("334455667"));
        Assert.That(foodSeller2.Address, Is.EqualTo("NewStreet 2, Copenhagen"));
        Assert.That(foodSeller2.Name, Is.EqualTo("NewName"));
    }
}