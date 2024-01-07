using Application.Features.Supports.Commands.Create;
using Application.Features.Supports.Commands.Delete;
using Application.Features.Supports.Commands.Update;
using Application.Features.Supports.Queries.GetById;
using Application.Features.Supports.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Supports.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Support, CreateSupportCommand>().ReverseMap();
        CreateMap<Support, CreateSupportResponse>().ReverseMap();

        CreateMap<Support, DeleteSupportCommand>().ReverseMap();
        CreateMap<Support, DeleteSupportResponse>().ReverseMap();

        CreateMap<Support, UpdateSupportCommand>().ReverseMap();
        CreateMap<Support, UpdateSupportResponse>().ReverseMap();

        CreateMap<Support, GetByIdSupportResponse>().ReverseMap();

        CreateMap<Support, GetListSupportListItemResponse>().ReverseMap();
        CreateMap<Paginate<Support>, GetListResponse<GetListSupportListItemResponse>>().ReverseMap();

        CreateMap<User, GetByIdSupportUserResponseDto>().ReverseMap();
        CreateMap<User, GetListSupportUserResponseDto>().ReverseMap();
    }
}
