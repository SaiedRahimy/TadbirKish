using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Domain.Entities;

namespace TadbirKish.DataReception.Infrastructure.Persistence.Manager
{
    public class CacheCoverageManager : ICacheCoverageManager
    {
        #region Dependencies
        private readonly DataReceptionContext _context;

        private readonly ILogger<CacheCoverageManager> _logger;

        #endregion


        #region Propereties

        private static readonly ConcurrentDictionary<int, CoverageInfoDto> CachedCoverages = new ConcurrentDictionary<int, CoverageInfoDto>();

        #endregion


        #region Constructor
        public CacheCoverageManager(DataReceptionContext context, ILogger<CacheCoverageManager> logger)
        {
            _context = context;
            _logger = logger;


        }
        #endregion

        #region Public Methods

        public ConcurrentDictionary<int, CoverageInfoDto> GetCachedCoverages()
        {
            return CachedCoverages;

        }
        public void RegisterNewCoverages(CoverageInfoDto coverage)
        {
            CachedCoverages.AddOrUpdate(coverage.Id, coverage, (key, value) => coverage);
        }


        public async Task FillCoveragesCache()
        {
            try
            {
                var coverages = await _context.Coverages.Select(c => new CoverageInfoDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Min = c.Min,
                    Max = c.Max,
                    Coefficient = c.Coefficient,
                }).ToListAsync();

                coverages.ForEach(coverage =>
                {

                    CachedCoverages.AddOrUpdate(coverage.Id, coverage, (key, value) => coverage);

                });

                if (CachedCoverages.Count > 0)
                {
                    _logger.LogInformation(coverages.Count + " has cached in FillCoveragesCache");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Fill Coverages Cache failed: ", ex);
            }
        }
        #endregion

    }
}
