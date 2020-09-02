using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Apartment.API.Infrastructure.Commands
{
    public class UpdatePicCommandHandler
        : IRequestHandler<UpdatePicCommand, bool>
    {
        private readonly ApartmentContext _context;
        private readonly ILogger<UpdatePicCommandHandler> _logger;

        public UpdatePicCommandHandler(ApartmentContext context, ILogger<UpdatePicCommandHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdatePicCommand request, CancellationToken cancellationToken)
        {
            var apartment = await _context.Apartment.FirstOrDefaultAsync(a => a.RequestId == request.RequestId);
            if (apartment == null) return false;
            apartment.PictureUri = request.PictureUri;
            apartment.PictureFileName = request.PictureFileName;
            _context.Apartment.Update(apartment);
            _logger.LogInformation($"Starting Updating Pic info for request No: {request.RequestId} - Command: {request}");
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
