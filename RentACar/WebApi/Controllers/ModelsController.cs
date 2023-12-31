using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistance.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    public class ModelsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetListModelQuery getListModelQuery)
        {
            var result = await Mediator.Send(getListModelQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery=null)
        {
            var result = await Mediator.Send(new GetListByDynamicModelQuery() { DynamicQuery = dynamicQuery, PageRequest = pageRequest });
            return Ok(result);
        }
    }
}
