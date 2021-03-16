using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class List
    {
        public class Query : IRequest<List<Photo>> { }

        public class Handler : IRequestHandler<Query, List<Photo>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Photo>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ph = await _context.Photos.ToListAsync();

                return ph;
            }
        }
    }
}