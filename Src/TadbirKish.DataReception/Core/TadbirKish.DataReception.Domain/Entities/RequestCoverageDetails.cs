using TadbirKish.DataReception.Domain.Common;

namespace TadbirKish.DataReception.Domain.Entities
{
    public class RequestCoverageDetails : AuditableEntity
    {
        public long Id { get; set; }
        public int RequestCoverageId { get; set; }
        public virtual RequestCoverage RequestCoverage { get; set; }

        public int CoverageId { get; set; }
        public virtual Coverage Coverage { get; set; }

        public long GrossPremium { get; set; }
        public double NetPremium { get; set; }


    }
}
