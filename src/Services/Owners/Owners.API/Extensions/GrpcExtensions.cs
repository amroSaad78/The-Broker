using Owners.API.Model;
using OwnersAPI.Proto;

namespace Owners.API.Grpc
{
    public static class GrpcExtensions
    {
        public static OwnerBasicResponse MapToGrpcOwnerBasic(this Owner owner) => new OwnerBasicResponse()
        {
            Id = owner.Id,
            FullName = $"{owner.FirstName} {owner.LastName}"
        };

        public static OwnerResponse MapToGrpcOwner(this Owner owner) => new OwnerResponse()
        {
            Id = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Email = owner.Email,
            Company = owner.Company,
            Address = owner.Address,
            City = owner.City,
            Mobile = owner.Mobile,
            ZIP = owner.ZIP
        };
    }
}
