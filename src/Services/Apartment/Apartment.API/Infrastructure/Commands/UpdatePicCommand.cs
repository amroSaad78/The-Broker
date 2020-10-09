using MediatR;
using System;
using System.Runtime.Serialization;

namespace Apartment.API.Infrastructure.Commands
{
    public class UpdatePicCommand: IRequest<bool>
    {
        public UpdatePicCommand()
        {

        }
        public UpdatePicCommand(string pictureUri, string pictureFileName, Guid requestId)
        {
            RequestId = requestId;
            PictureUri = pictureUri;
            PictureFileName = pictureFileName;
        }
        [DataMember]
        public Guid RequestId { get; private set; }
        [DataMember]
        public string PictureUri { get; private set; }
        [DataMember]
        public string PictureFileName { get; private set; }
    }
}
