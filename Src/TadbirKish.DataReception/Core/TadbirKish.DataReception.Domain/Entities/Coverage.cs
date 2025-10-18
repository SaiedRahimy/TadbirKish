using TadbirKish.DataReception.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Domain.Entities
{
    public class Coverage : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Min { get; set; }
        public long Max { get; set; }
        public double Coefficient { get; set; }

        public ICollection<RequestCoverageDetails> AllRequestCoverageDetails { get; set; }

    }
}
