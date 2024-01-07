﻿namespace Application.Features.Supports.Queries.GetList;

public class GetListSupportUserResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string IdentityNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int FindexScore { get; set; }
}