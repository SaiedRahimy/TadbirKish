using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Dtos
{
    public class RequestCoverageDetailsInfoDto
    {
        public int RequestCoverageId { get; set; }

        public int CoverageId { get; set; }
        public long GrossPremium { get; set; }
        public double NetPremium { get; set; }

    }
}
