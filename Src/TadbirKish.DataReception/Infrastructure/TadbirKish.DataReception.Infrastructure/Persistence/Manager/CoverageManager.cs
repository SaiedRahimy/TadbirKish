using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Common.Exceptions;
using TadbirKish.DataReception.Application.Common.Interfaces;
using TadbirKish.DataReception.Application.Common.Models;
using TadbirKish.DataReception.Application.Dtos;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.CreateCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.DeleteCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Commands.UpdateCoverage;
using TadbirKish.DataReception.Application.Entities.Coverage.Queries.GetCoverage;
using TadbirKish.DataReception.Domain.Entities;

namespace TadbirKish.DataReception.Infrastructure.Persistence.Manager
{
    public class CoverageManager : ICoverageManager
    {
        #region Dependencies
        private readonly DataReceptionContext _context;

        private readonly ILogger<CoverageManager> _logger;
        private readonly ICacheCoverageManager _cacheCoverageManager;

        #endregion


        #region Propereties
       
        #endregion


        #region Ctor
        public CoverageManager(DataReceptionContext context, ILogger<CoverageManager> logger, ICacheCoverageManager cacheCoverageManager)
        {
            _context = context;
            _logger = logger;
            _cacheCoverageManager = cacheCoverageManager;   

        }
        #endregion

        #region Public Methods
        public async Task<bool> CreateCoverage(CreateCoverageCommand request)
        {
            // add log
            var entity = new Coverage
            {
                Name = request.Name,
                Min = request.Min,
                Max = request.Max,
                Coefficient = request.Coefficient,

                 
            };
         var result=   await _context.Coverages.AddAsync(entity);

            await _context.SaveChangesAsync();

            _cacheCoverageManager.RegisterNewCoverages(new CoverageInfoDto {
                Id = result.Entity.Id,
                Min = result.Entity.Min,
                Max = result.Entity.Max,
                Coefficient = result.Entity.Coefficient,
            });

            _logger.LogInformation($"Register Coverage {request.Name}");

            return true;
        }



        public async Task<bool> DeleteCoverage(DeleteCoverageCommand request)
        {
            await CheckIdExist(request.Id);

            var entity = await _context.Coverages.FirstOrDefaultAsync(c => c.Id == request.Id);

            _context.Coverages.Remove(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Delete Coverage {request.Id}");
            return true;
        }



        public async Task<CoverageInfoDto> GetCoverage(GetCoverageQuery request)
        {
            var result = await _context.Coverages.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (result is not null)
            {
                return new CoverageInfoDto()
                {
                    Id = result.Id,
                    Name = result.Name
                };
            }
            else
                throw new NotFoundException($"Could not find value with id {request.Id}.");
        }



        public async Task<bool> UpdateCoverage(UpdateCoverageCommand request)
        {
            await CheckIdExist(request.Id);
            var entity = await _context.Coverages.FirstOrDefaultAsync(c => c.Id == request.Id);

            entity.Name = request.Name;
            entity.Min = request.Min;
            entity.Max = request.Max;
            entity.Coefficient = request.Coefficient;


            _context.Update(entity);
            await _context.SaveChangesAsync();

            _cacheCoverageManager.RegisterNewCoverages(new CoverageInfoDto
            {
                Id = entity.Id,
                Min = entity.Min,
                Max = entity.Max,
                Coefficient = entity.Coefficient,
            });

            _logger.LogInformation($"Update Coverage {request.Name}");

            return true;
        }



        private async Task CheckIdExist(int id)
        {
            if (!await _context.Coverages.AnyAsync(c => c.Id == id))
            {
                throw new NotFoundException($"Could not find Coverage with id {id}.");
            }
        }

        #endregion

    }
}
