using Google.Protobuf.Collections;
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
        private readonly OwnerSettings _settings;
        private readonly ILogger<OwnerService> _logger;

        public OwnerService(OwnerContext dbContext, IOptions<OwnerSettings> settings, ILogger<OwnerService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
            _settings = settings.Value;
            _logger = logger;
        }

        public override async Task<OwnerResponse> GetOwnerById(OwnerRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call OwnersService.GetOwnerById for owner id {Id}", request.Id);
            if (request.Id <= 0)
            {
                context.Status = new Status(StatusCode.FailedPrecondition, $"Id must be > 0 (received {request.Id})");
                return null;
            }
            var owner = await _dbContext.Owners.SingleOrDefaultAsync(ci => ci.Id == request.Id);

            if (owner != null)
            {
                return new OwnerResponse()
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    LastName=owner.LastName,
                    Email=owner.Email,
                    Mobile=owner.Mobile,
                    Address=owner.Address,
                    ZIP=owner.ZIP,
                    City=owner.City,
                    Company = owner.Company
                };
            }

            context.Status = new Status(StatusCode.NotFound, $"Owner with id {request.Id} do not exist");
            return null;
        }

        public override async Task<OwnersResponse> GetOwners(OwnersRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call Owners.GetOwners");
            var owners = await _dbContext.Owners.OrderBy(o => o.FirstName).ToListAsync();
            if (owners != null)
            {
                var GrpcOwners = new RepeatedField<OwnerResponse>();
                owners.ForEach(o =>
                {
                    GrpcOwners.Add(new OwnerResponse
                    {
                        Id = o.Id,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Email = o.Email,
                        Address = o.Address,
                        ZIP = o.ZIP,
                        City = o.City,
                        Company = o.Company,
                        Mobile = o.Mobile
                    });
                });
                var OwnersResponse = new OwnersResponse() { };
                OwnersResponse.GrpcOwners.AddRange(GrpcOwners);
                return OwnersResponse;
            }
            context.Status = new Status(StatusCode.NotFound, $"No owners data are found.");
            return null;
        }

    }
}
