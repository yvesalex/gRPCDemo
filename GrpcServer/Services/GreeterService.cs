using Grpc.Core;
using Grpc.Net.Client;
using Metrics;

namespace GrpcServer.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    /* version for greetings only
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = $"Hello {request.Name}!"
            });
        }
    */
    
    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        // Create channel to communicate with NameMetrics service
        using var channel = GrpcChannel.ForAddress("http://localhost:5258");

        // Create gRPC client
        var metricsClient = new NameMetrics.NameMetricsClient(channel);

        // Call the GetLength method
        var metricsReply = await metricsClient.GetLengthAsync(new LengthRequest { Name = request.Name });

        // Build the response
        var message = $"Hello {request.Name}! Your name has {metricsReply.Length} characters.";
        return new HelloReply { Message = message };
    }
}
