using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Dtos
{
    public class RequestCoverageInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RequestCoverageDetailsInfoDto> RequestCoverageDetails { get; set; }
        public long GrossPremium { get; set; }
        public double TotalNetPremium { get; set; }

    }
}
