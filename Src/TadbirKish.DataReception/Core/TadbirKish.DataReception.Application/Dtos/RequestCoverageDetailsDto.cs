using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Dtos
{
    public class RequestCoverageDetailsDto
    {
        public int CoverageId { get; set; }
        public long GrossPremium { get; set; }
    }
}
