//using Bouvet.AssetHub.Domain.Dtos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bouvet.AssetHub.Domain.Handlers
//{
//    public class GetAssets
//    {
//        public record Request() : IRequest<List<AssetsResponse>>;

//        public class Handler : IRequestHandler<Request, List<AssetsResponse>>
//        {
//            private readonly Serilog.ILogger _log;
//            private readonly IScoringService _scoringService;

//            public Handler(IScoringService scoringService, Serilog.ILogger log)
//            {
//                _log = log;
//                _scoringService = scoringService;
//            }

//            public async Task<List<ResultDto>> Handle(Request request, CancellationToken cancellationToken)
//            {
//                _log.Information($"Pipeline.GetResults: Type - {request.type}. Sex - {request.sex}");
//                return await _scoringService.GetResultsByType(request.type, request.sex, cancellationToken);

//            }

//        }
//    }
//}
