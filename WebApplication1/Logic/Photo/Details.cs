using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace WebApplication1.Models
{
    public class Details
    {
        public class Query : IRequest<Photo>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Photo>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Photo> Handle(Query request, CancellationToken cancellationToken)
            {
                var photo = await _context.Photos.FindAsync(request.Id);

                if (photo == null) throw new Exception("Not found");

                return photo;
            }
        }
    }
}