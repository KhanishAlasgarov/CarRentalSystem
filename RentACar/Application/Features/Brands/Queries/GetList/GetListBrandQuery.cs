using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<GetListResponse<GetListBrandListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } = default!;

    public string CacheKey => $"GetListBrandQuery({PageRequest.PageIndex}, {PageRequest.PageSize})";

    public bool BypassCache { get; }

    public TimeSpan? SlidingExpiration { get; }

    public string? CacheGroupKey => "GetBrands";

    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandListItemDto>>
    {
        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        private IBrandRepository _brandRepository { get; }
        private IMapper _mapper { get; }
        public async Task<GetListResponse<GetListBrandListItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            Paginate<Brand> brands = await _brandRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            GetListResponse<GetListBrandListItemDto> response = _mapper.Map<GetListResponse<GetListBrandListItemDto>>(brands);

            return response;

        }
    }

}
