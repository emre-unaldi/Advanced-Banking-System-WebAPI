using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();

        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdateUserResponse>().ReverseMap();

        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, DeleteUserResponse>().ReverseMap();

        CreateMap<User, GetByIdUserResponse>().ReverseMap();

        CreateMap<User, GetListUserListItemResponse>().ReverseMap(); 
        CreateMap<Paginate<User>, GetListResponse<GetListUserListItemResponse>>().ReverseMap();

        CreateMap<Account, GetByIdUserAccountResponseDto>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()))
            .ReverseMap();
        CreateMap<Account, GetListUserAccountResponseDto>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()))
            .ReverseMap();
    }
}
