using TadbirKish.DataReception.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Domain.Entities
{
    public class RequestCoverage : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalNetPremium { get; set; }

        public ICollection<RequestCoverageDetails> AllRequestCoverageDetails { get; set; }

    }
}
