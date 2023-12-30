using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class BrandCreateCommand : IRequest<CreatedBrandResponse>
{
    public string? Name { get; set; }


    public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, CreatedBrandResponse>
    {
        public BrandCreateCommandHandler(IBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private IMapper _mapper { get; }
        private IBrandRepository _repository { get; }
        public async Task<CreatedBrandResponse> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
        {
            var brand = _mapper.Map<Brand>(request);
            brand.Id = Guid.NewGuid();

            return _mapper.Map<CreatedBrandResponse>(await _repository.AddAsync(brand));
        }
    }
}
