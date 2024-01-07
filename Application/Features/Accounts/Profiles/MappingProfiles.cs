using Application.Features.Accounts.Commands.Create;
using Application.Features.Accounts.Commands.Delete;
using Application.Features.Accounts.Commands.Update;
using Application.Features.Accounts.Queries.GetById;
using Application.Features.Accounts.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Accounts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Account, CreateAccountCommand>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => (int)src.AccountType))
            .ReverseMap();
        CreateMap<Account, CreateAccountResponse>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()))
            .ReverseMap();

        CreateMap<Account, UpdateAccountCommand>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => (int)src.AccountType))
            .ReverseMap();
        CreateMap<Account, UpdateAccountResponse>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()))
            .ReverseMap();

        CreateMap<Account, DeleteAccountCommand>().ReverseMap();
        CreateMap<Account, DeleteAccountResponse>().ReverseMap();

        CreateMap<Account, GetByIdAccountResponse>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()))
            .ReverseMap();

        CreateMap<Account, GetListAccountListItemResponse>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()))
            .ReverseMap();
        CreateMap<Paginate<Account>, GetListResponse<GetListAccountListItemResponse>>().ReverseMap();

        CreateMap<User, GetByIdAccountUserResponseDto>().ReverseMap();
        CreateMap<User, GetListAccountUserResponseDto>().ReverseMap();
    }
}
