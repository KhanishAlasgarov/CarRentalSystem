using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules : BaseBusinessRules
{
    private IBrandRepository _brandRepository { get; }

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    internal async Task BrandNameCannotBeDuplicatedWhenInserted(string name)
    {
        Brand? result = await _brandRepository.GetAsync(expression: b => b.Name.ToLower() == name.ToLower());

        if (result != null)
        {
            throw new BusinessException(BrandsMessages.BrandNameExists);
        }
    }
}
