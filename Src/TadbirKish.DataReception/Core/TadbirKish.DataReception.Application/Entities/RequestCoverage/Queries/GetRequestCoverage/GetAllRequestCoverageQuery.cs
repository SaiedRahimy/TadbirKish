using MediatR;
using TadbirKish.DataReception.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Entities.RequestCoverage.Queries.GetCoverage
{
    public class GetAllRequestCoverageQuery : IRequest<List<RequestCoverageInfoDto>>
    {
    }
}
