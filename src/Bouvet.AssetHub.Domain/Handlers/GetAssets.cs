using Bouvet.AssetHub.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Handlers
{
    public class GetAssets
    {
        public record Request() : IRequest<List<AssetResponse>>;

        public class Handler : IRequestHandler<Request, List<AssetResponse>>
        {
            
            public Handler(DataContext context)
            {
                this.context = context;
                
            }

            public async Task<List<AssetResponse>> Handle(Request request, CancellationToken cancellationToken)
            {
                _log.Information($"Pipeline.GetResults: Type - {request.type}. Sex - {request.sex}");
                return await _scoringService.GetResultsByType(request.type, request.sex, cancellationToken);

            }

        }
    }
}
