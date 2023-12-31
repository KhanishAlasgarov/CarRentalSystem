using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using Core.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    public class BrandsController : BaseController
    {
        

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdBrandQuery request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListBrandQuery request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandCreateCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBrandCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] DeleteBrandCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
