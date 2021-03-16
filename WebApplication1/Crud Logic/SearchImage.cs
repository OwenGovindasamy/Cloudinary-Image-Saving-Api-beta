using System;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using MediatR;
using WebApplication1.Models.Interfaces;

namespace WebApplication1.Models
{
    public class SearchImage
    {
        public class Query : IRequest<SearchResult>
        {
            public string paramsk { get; set; }
        }

        public class Handler : IRequestHandler<Query, SearchResult>
        {
           private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAcessor;
            public Handler(DataContext context, IPhotoAccessor photoAcessor)
            {
                _photoAcessor = photoAcessor;
                _context = context;
            }

            public async Task<SearchResult> Handle(Query request, CancellationToken cancellationToken) 
            // The cloudinary search method is not available in async
            {
                var SearchResult = _photoAcessor.SearchPhoto(request.paramsk);

                if (SearchResult == null) throw new Exception("Not found");

                return SearchResult;
            }
        }
    }
}