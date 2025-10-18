using TadbirKish.DataReception.Application.Common.Interfaces;
using System;

namespace TadbirKish.DataReception.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
