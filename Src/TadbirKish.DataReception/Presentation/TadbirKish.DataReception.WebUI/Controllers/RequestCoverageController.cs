using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Application.Entities.RequestCoverage.Commands.CreateCoverage;
using TadbirKish.DataReception.Application.Entities.RequestCoverage.Queries.GetCoverage;

namespace TadbirKish.DataReception.WebUI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RequestCoverageController : ApiControllerBase
    {
        public RequestCoverageController()
        {

        }

        [HttpPost]
        [Route("RequestCoverages")]
        public async Task<BaseApiResponseDto<bool>> Create([FromBody] CreateRequestCoverageCommand command)
        {
            var result = await Mediator.Send(command);
            return new BaseApiResponseDto<bool>(result, true);
        }

        [HttpGet]
        [Route("RequestCoverages/{id}")]
        public async Task<BaseApiResponseDto<RequestCoverageInfoDto>> GetCoverage([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetRequestCoverageQuery { Id = id });
            return new BaseApiResponseDto<RequestCoverageInfoDto>(result, true);
        }


        [HttpGet]
        [Route("RequestCoverages")]
        public async Task<BaseApiResponseDto<List<RequestCoverageInfoDto>>> GetCoverage()
        {
            var result = await Mediator.Send(new GetAllRequestCoverageQuery { });
            return new BaseApiResponseDto<List<RequestCoverageInfoDto>>(result, true);
        }
    }
}
