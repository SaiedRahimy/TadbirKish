using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Common.Exceptions;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Application.Entities.RequestCoverage.Commands.CreateCoverage;
using TadbirKish.DataReception.Application.Entities.RequestCoverage.Queries.GetCoverage;
using TadbirKish.DataReception.Domain.Entities;

namespace TadbirKish.DataReception.Infrastructure.Persistence.Manager
{
    public class RequestCoverageManager : IRequestCoverageManager
    {
        #region Dependencies
        private readonly DataReceptionContext _context;

        private readonly ILogger<RequestCoverageManager> _logger;

        #endregion


        #region Propereties

        #endregion


        #region Ctor
        public RequestCoverageManager(DataReceptionContext context, ILogger<RequestCoverageManager> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task<bool> CreateRequestCoverage(CreateRequestCoverageCommand request)
        {

            var coverages = await _context.Coverages.Where(c => request.RequestCoverageDetails.Select(r => r.CoverageId).ToList().Contains(c.Id)).ToListAsync();


            var coverageDetails = new List<RequestCoverageDetails>();
            foreach (var requestCoverage in request.RequestCoverageDetails)
            {
                var coverage = coverages.First(c => c.Id == requestCoverage.CoverageId);

                coverageDetails.Add(new RequestCoverageDetails
                {
                    CoverageId = requestCoverage.CoverageId,
                    GrossPremium = requestCoverage.GrossPremium,
                    NetPremium = requestCoverage.GrossPremium * coverage.Coefficient,
                });

            }

            var entity = new RequestCoverage
            {
                Name = request.Name,
                TotalNetPremium = coverageDetails.Sum(c => c.NetPremium),
            };

            var result = await _context.RequestCoverages.AddAsync(entity);

            foreach (var detail in coverageDetails)
            {
                detail.RequestCoverage = result.Entity;
            }

            await _context.RequestCoverageDetails.AddRangeAsync(coverageDetails);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Register Coverage {request.Name}");

            return true;
        }




        public async Task<RequestCoverageInfoDto> GetRequestCoverages(GetRequestCoverageQuery request)
        {
            var result = await _context.RequestCoverages
                .Include(c => c.AllRequestCoverageDetails)
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (result is not null)
            {
                return new RequestCoverageInfoDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    GrossPremium = result.AllRequestCoverageDetails.Sum(rcd => rcd.GrossPremium),
                    TotalNetPremium = result.TotalNetPremium,
                    RequestCoverageDetails = result.AllRequestCoverageDetails.Select(rcd => new RequestCoverageDetailsInfoDto
                    {
                        CoverageId = rcd.CoverageId,
                        RequestCoverageId = rcd.RequestCoverageId,
                        GrossPremium = rcd.GrossPremium,
                        NetPremium = rcd.NetPremium
                    }).ToList(),

                };
            }
            else
                throw new NotFoundException($"Could not find value with id {request.Id}.");
        }



        public async Task<List<RequestCoverageInfoDto>> GetAllRequestCoverages(GetAllRequestCoverageQuery request)
        {
            return await _context.RequestCoverages
                .Include(c => c.AllRequestCoverageDetails)
                .Select(c => new RequestCoverageInfoDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    GrossPremium = c.AllRequestCoverageDetails.Sum(rcd => rcd.GrossPremium),
                    TotalNetPremium = c.TotalNetPremium,
                    RequestCoverageDetails = c.AllRequestCoverageDetails.Select(c => new RequestCoverageDetailsInfoDto
                    {
                        CoverageId = c.CoverageId,
                        RequestCoverageId = c.RequestCoverageId,
                        GrossPremium = c.GrossPremium,
                        NetPremium = c.NetPremium
                    }).ToList(),

                })
                .ToListAsync();

        }




        #endregion

    }
}
