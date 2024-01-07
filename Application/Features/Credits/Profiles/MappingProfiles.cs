using Application.Features.Credits.Commands.Application;
using Application.Features.Credits.Commands.Approval;
using Application.Features.Credits.Commands.Delete;
using Application.Features.Credits.Commands.Update;
using Application.Features.Credits.Queries.GetById;
using Application.Features.Credits.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Credits.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Credit, ApplicationCreditCommand>().ReverseMap();
        CreateMap<Credit, ApplicationCreditResponse>().ReverseMap();

        CreateMap<Credit, ApprovalCreditCommand>().ReverseMap();
        CreateMap<Credit, ApprovalCreditResponse>().ReverseMap();

        CreateMap<Credit, UpdateCreditCommand>().ReverseMap();
        CreateMap<Credit, UpdateCreditResponse>().ReverseMap();

        CreateMap<Credit, DeleteCreditCommand>().ReverseMap();
        CreateMap<Credit, DeleteCreditResponse>().ReverseMap();

        CreateMap<Credit, GetByIdCreditResponse>().ReverseMap();
        CreateMap<User, GetByIdCreditUserResponseDto>().ReverseMap();

        CreateMap<Credit, GetListCreditListItemResponse>().ReverseMap();
        CreateMap<Paginate<Credit>, GetListResponse<GetListCreditListItemResponse>>().ReverseMap();
        CreateMap<User, GetListCreditUserResponseDto>().ReverseMap();
    }
}
