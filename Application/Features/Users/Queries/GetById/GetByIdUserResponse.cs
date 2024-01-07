using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse
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
    public List<GetByIdUserAccountResponseDto> Accounts { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
