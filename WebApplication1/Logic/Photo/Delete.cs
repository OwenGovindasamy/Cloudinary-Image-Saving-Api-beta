using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApplication1.Models.Interfaces;

namespace WebApplication1.Models
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _photoAccessor.DeletePhoto(request.Id);

                if (result == null)
                {
                    throw new Exception("Image not found");
                }
                return Unit.Value;
            }
        }
    }
}