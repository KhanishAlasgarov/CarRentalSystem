using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entities;

namespace Application.Features.Brands.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Brand, CreatedBrandResponse>().ReverseMap();
        CreateMap<Brand, BrandCreateCommand>().ReverseMap(); 
        CreateMap<Brand, GetListBrandListItemDto>().ReverseMap(); 
        CreateMap<Paginate<Brand>, GetListResponse<GetListBrandListItemDto>>().ReverseMap(); 
        CreateMap<GetByIdBrandResponse, Brand>().ReverseMap(); 
        CreateMap<UpdateBrandCommand, Brand>().ReverseMap();
        CreateMap<UpdatedBrandResponse, Brand>().ReverseMap();
        CreateMap<DeletedBrandResponse, Brand>().ReverseMap();
    }
}
