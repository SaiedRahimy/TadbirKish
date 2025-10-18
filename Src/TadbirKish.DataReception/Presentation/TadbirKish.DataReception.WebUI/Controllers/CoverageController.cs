using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.CreateCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.DeleteCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.UpdateCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Queries.GetCoverage;

namespace TadbirKish.DataReception.WebUI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CoverageController : ApiControllerBase
    {
        public CoverageController()
        {

        }

        [HttpPost]
        [Route("Coverages")]
        public async Task<BaseApiResponseDto<bool>> Create([FromBody] CreateCoverageCommand command)
        {
            var result = await Mediator.Send(command);
            return new BaseApiResponseDto<bool>(result, true);
        }

        [HttpDelete]
        [Route("Coverages/{id}")]
        public async Task<BaseApiResponseDto<bool>> DeleteTag([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteCoverageCommand { Id = id });
            return new BaseApiResponseDto<bool>(result, true);
        }

        [HttpPut]
        [Route("Coverages/{id}")]
        public async Task<BaseApiResponseDto<bool>> UpdateTag([FromBody] UpdateCoverageCommand command)
        {
            var result = await Mediator.Send(command);
            return new BaseApiResponseDto<bool>(result, true);
        }

        [HttpGet]
        [Route("Coverages/{id}")]
        public async Task<BaseApiResponseDto<CoverageInfoDto>> GetCoverage([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetCoverageQuery { Id = id });
            return new BaseApiResponseDto<CoverageInfoDto>(result, true);
        }
    }
}
