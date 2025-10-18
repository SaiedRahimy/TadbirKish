using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TadbirKish.DataReception.Application.Dtos
{
    public class CoverageInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Min { get; set; }
        public long Max { get; set; }
        public double Coefficient { get; set; }
    }
}
