﻿namespace ComputerStore.Services.Auth.Api.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
