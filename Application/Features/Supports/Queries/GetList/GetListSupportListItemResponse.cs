namespace Application.Features.Supports.Queries.GetList;

public class GetListSupportListItemResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public GetListSupportUserResponseDto User { get; set; }
    public DateTime CreatedDate { get; set; }
}
