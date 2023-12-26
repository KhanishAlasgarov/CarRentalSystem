using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Create
{
    public class BrandCreateCommand : IRequest<CreatedBrandResponse>
    {
        public string? Name { get; set; }

        public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, CreatedBrandResponse>
        {
            public async Task<CreatedBrandResponse> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
            {
                 
            }
        }
    }
}
