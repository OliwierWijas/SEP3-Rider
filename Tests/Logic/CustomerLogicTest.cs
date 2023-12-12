using Application.Logic;
using Domain.DTOs;

namespace Tests.Logic;

public class CustomerLogicTest
{
    private CustomerLogic _logic;
        
    [SetUp]
    public void Setup()
    {
        _logic = new CustomerLogic(new GRPCService());
    }

    [Test]
    public async Task creating_customer_makes_an_account()
    {
        CustomerCreationDTO dto = new CustomerCreationDTO("FirstName", "LastName", "Email@via.dk", "111222333", "Password123");
        await _logic.CreateAsync(dto);
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "Email@via.dk",
            Password = "Password123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(20, Is.EqualTo(user.Id));
        Assert.That(user.Email, Is.EqualTo("Email@via.dk"));
        Assert.That(user.Password, Is.EqualTo("Password123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("111222333"));
        Assert.That(user.Address, Is.EqualTo(""));
        Assert.That(user.Name, Is.EqualTo(""));
        Assert.That(user.FirstName, Is.EqualTo("FirstName"));
        Assert.That(user.LastName, Is.EqualTo("LastName"));
        Assert.That(user.Type, Is.EqualTo("customer"));
    }

    [Test]
    public async Task creating_customer_with_existing_email_throws_an_exception()
    {
        CustomerCreationDTO dto = new CustomerCreationDTO("FirstName", "LastName", "Email@via.dk", "000000000", "Password123");
        Assert.CatchAsync<Exception>(() => _logic.CreateAsync(dto));
    }
    
    [Test]
    public async Task creating_customer_with_existing_phone_number_throws_an_exception()
    {
        CustomerCreationDTO dto = new CustomerCreationDTO("FirstName", "LastName", "Email1@via.dk", "111222333", "Password123");
        Assert.CatchAsync<Exception>(() => _logic.CreateAsync(dto));
    }

    [Test]
    public async Task updating_email_changes_data()
    {
        CustomerUpdateDTO update = new CustomerUpdateDTO(20, "NewEmail1@via.dk", null, null);
        Assert.DoesNotThrowAsync(() => _logic.UpdateEmail(update));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "NewEmail1@via.dk",
            Password = "Password123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(user.Id, Is.EqualTo(20));
        Assert.That(user.Email, Is.EqualTo("NewEmail1@via.dk"));
        Assert.That(user.Password, Is.EqualTo("Password123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("111222333"));
        Assert.That(user.Address, Is.EqualTo(""));
        Assert.That(user.Name, Is.EqualTo(""));
        Assert.That(user.FirstName, Is.EqualTo("FirstName"));
        Assert.That(user.LastName, Is.EqualTo("LastName"));
        Assert.That(user.Type, Is.EqualTo("customer"));
    }
    
    [Test]
    public async Task updating_phone_number_changes_data()
    {
        CustomerUpdateDTO update = new CustomerUpdateDTO(20, null, null, "222444666");
        Assert.DoesNotThrowAsync(() => _logic.UpdatePhoneNumber(update));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "NewEmail1@via.dk",
            Password = "Password123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(user.Id, Is.EqualTo(20));
        Assert.That(user.Email, Is.EqualTo("NewEmail1@via.dk"));
        Assert.That(user.Password, Is.EqualTo("Password123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("222444666"));
        Assert.That(user.Address, Is.EqualTo(""));
        Assert.That(user.Name, Is.EqualTo(""));
        Assert.That(user.FirstName, Is.EqualTo("FirstName"));
        Assert.That(user.LastName, Is.EqualTo("LastName"));
        Assert.That(user.Type, Is.EqualTo("customer"));
    }
    
    [Test]
    public async Task updating_password_changes_data()
    {
        CustomerUpdateDTO update = new CustomerUpdateDTO(20, null, "NewPassword123", null);
        Assert.DoesNotThrowAsync(() => _logic.UpdatePassword(update));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "NewEmail1@via.dk",
            Password = "NewPassword123"
        };
        UserBasicDTO user = await loginLogic.LoginAsync(loginDto);
        Assert.That(user.Id, Is.EqualTo(20));
        Assert.That(user.Email, Is.EqualTo("NewEmail1@via.dk"));
        Assert.That(user.Password, Is.EqualTo("NewPassword123"));
        Assert.That(user.PhoneNumber, Is.EqualTo("222444666"));
        Assert.That(user.Address, Is.EqualTo(""));
        Assert.That(user.Name, Is.EqualTo(""));
        Assert.That(user.FirstName, Is.EqualTo("FirstName"));
        Assert.That(user.LastName, Is.EqualTo("LastName"));
        Assert.That(user.Type, Is.EqualTo("customer"));
    }
    
        
    [Test]
    public async Task deleting_account_removes_data()
    {
        Assert.DoesNotThrowAsync(() => _logic.DeleteAccount(20));
        LoginLogic loginLogic = new LoginLogic(new GRPCService());
        LoginDTO loginDto = new LoginDTO
        {
            Email = "NewEmail1@via.dk",
            Password = "NewPassword123"
        };
        Assert.CatchAsync<Exception>(() => loginLogic.LoginAsync(loginDto));
    }
}