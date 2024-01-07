using Domain.Entities;

namespace Application.Features.Supports.Queries.GetById;

public class GetByIdSupportResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public GetByIdSupportUserResponseDto User { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
