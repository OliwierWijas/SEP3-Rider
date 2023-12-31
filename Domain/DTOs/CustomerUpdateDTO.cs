﻿namespace Domain.DTOs;

public class CustomerUpdateDTO
{
    public int AccountId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }


    public CustomerUpdateDTO(int accountId, string? email, string? password, string? phoneNumber)
    {
        AccountId = accountId;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
    }
}