using Application.Logic;
using Domain.DTOs;

namespace Tests.Logic;

public class LoginLogicTest
{
    private LoginLogic _logic;
        
    [SetUp]
    public void Setup()
    {
        _logic = new LoginLogic(new GRPCService());
    }

    [Test]
    public async Task logging_with_existing_account_returns_correct_data()
    {
        LoginDTO loginDto = new LoginDTO
        {
            Email = "email@via.dk",
            Password = "Password123"
        };
        UserBasicDTO dto = await _logic.LoginAsync(loginDto);
        Assert.That(dto.Id, Is.EqualTo(10));
        Assert.That(dto.Email, Is.EqualTo("email@via.dk"));
        Assert.That(dto.Password, Is.EqualTo("Password123"));
        Assert.That(dto.PhoneNumber, Is.EqualTo("1234567890"));
        Assert.That(dto.Address, Is.EqualTo(""));
        Assert.That(dto.Name, Is.EqualTo(""));
        Assert.That(dto.FirstName, Is.EqualTo("firstName"));
        Assert.That(dto.LastName, Is.EqualTo("lastName"));
        Assert.That(dto.Type, Is.EqualTo("customer"));
    }

    [Test]
    public async Task logging_with_non_existing_account_throws_an_exception()
    {
        LoginDTO loginDto = new LoginDTO
        {
            Email = "nonexisting@via.dk",
            Password = "Password123"
        };
        Assert.CatchAsync<Exception>(() => _logic.LoginAsync(loginDto));
    }
}