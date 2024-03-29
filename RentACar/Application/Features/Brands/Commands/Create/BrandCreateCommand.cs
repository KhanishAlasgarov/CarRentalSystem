﻿using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;

namespace Application.Features.Brands.Commands.Create;

public class BrandCreateCommand : IRequest<CreatedBrandResponse>, ITransactionalRequest, ICacheRemoverRequest,ILoggableRequest
{
    public string Name { get; set; } = default!;
    public string? CacheGroupKey => "GetBrands";

    public string? CacheKey => "";

    public bool BypassCache => false;

    public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, CreatedBrandResponse>
    {
        public BrandCreateCommandHandler(IBrandRepository repository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _repository = repository;
            _mapper = mapper;
            this.brandBusinessRules = brandBusinessRules;
        }
        private IMapper _mapper { get; }
        private IBrandRepository _repository { get; }
        private BrandBusinessRules brandBusinessRules { get; }
        public async Task<CreatedBrandResponse> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
        {

            await brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

            var brand = _mapper.Map<Brand>(request);
            brand.Id = Guid.NewGuid();
            var responseBrand =await _repository.AddAsync(brand);
            return _mapper.Map<CreatedBrandResponse>(responseBrand);
        }
    }
}
