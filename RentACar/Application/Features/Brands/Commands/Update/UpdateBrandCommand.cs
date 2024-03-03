using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest<UpdatedBrandResponse>,ICacheRemoverRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public string? CacheGroupKey => "GetBrands";

    public string? CacheKey => "";

    public bool BypassCache => false;

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdatedBrandResponse>
    {
        public UpdateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        private IMapper _mapper { get; }
        private IBrandRepository _brandRepository { get; }
        public async Task<UpdatedBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(expression: x => x.Id == request.Id,
                                                cancellationToken: cancellationToken);


            brand = _mapper.Map(request, brand);


            var response = await _brandRepository.UpdateAsync(brand);

            return _mapper.Map<UpdatedBrandResponse>(response);
        }
    }
}
