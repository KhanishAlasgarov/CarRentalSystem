using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<DeletedBrandResponse>,ICacheRemoverRequest
{
    public Guid Id { get; set; }


    public string? CacheKey => "";

    public bool BypassCache => false;

    public string? CacheGroupKey => "GetBrands";

    public class DelteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeletedBrandResponse>
    {
        public DelteBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        private IMapper _mapper { get; }
        private IBrandRepository _brandRepository { get; }
        public async Task<DeletedBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(expression: b => b.Id == request.Id,
                cancellationToken: cancellationToken);

            if (brand == null)
                throw new Exception();

            var response = await _brandRepository.DeleteAsync(brand);

            return _mapper.Map<DeletedBrandResponse>(response);
        }
    }
}
