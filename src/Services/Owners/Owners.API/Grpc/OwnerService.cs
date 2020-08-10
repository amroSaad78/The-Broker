using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Owners.API.Infrastructure;
using OwnersAPI.Proto;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OwnersAPI.Proto.OwnersData;

namespace Owners.API.Grpc
{
    public class OwnerService: OwnersDataBase
    {
        private readonly OwnerContext _dbContext;
        private readonly ILogger<OwnerService> _logger;

        public OwnerService(OwnerContext dbContext, ILogger<OwnerService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
            _logger = logger;
        }

        public override async Task<OwnersBasicResponse> GetBasicOwners(OwnersRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call Owners.GetBasicOwners");
            var owners = await _dbContext.Owners?.Select(o => o.MapToGrpcOwnerBasic()).ToListAsync();
            var GrpcOwners = new OwnersBasicResponse();
            GrpcOwners.GrpcOwnersBasic.AddRange(owners);
            return GrpcOwners;
        }

        public override async Task<OwnersResponse> GetOwners(OwnersRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call Owners.GetOwners");
            var owners = await _dbContext.Owners?.Select(o => o.MapToGrpcOwner()).ToListAsync();
            var GrpcOwners = new OwnersResponse();
            GrpcOwners.GrpcOwners.AddRange(owners);
            return GrpcOwners;
        }

        public override async Task<OwnerResponse> GetOwnerById(OwnerRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Begin grpc call Owners.GetOwnerById({request.Id})");
            if (request.Id <= 0)
            {
                context.Status = new Status(StatusCode.FailedPrecondition, $"Id must be > 0 (received {request.Id})");
                return null;
            }
            var owner = await _dbContext.Owners.SingleOrDefaultAsync(o => o.Id == request.Id);
            if (owner != null)
            {
                return owner.MapToGrpcOwner();
            }
            context.Status = new Status(StatusCode.NotFound, $"Owner with id {request.Id} do not exist");
            return null;
        }
    }
}
