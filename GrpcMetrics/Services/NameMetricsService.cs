using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Metrics;

namespace GrpcMetrics.Services
{

public class NameMetricsService : NameMetrics.NameMetricsBase
{
    public override Task<LengthReply> GetLength(LengthRequest request, ServerCallContext context)
    {
        int length = request.Name?.Length ?? 0;
        return Task.FromResult(new LengthReply { Length = length });
    }
}

}