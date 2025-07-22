using Grpc.Net.Client;
using Greet;
using Metrics;




Console.Write("Enter your name: ");
var name = Console.ReadLine();

//service 1: greet
var channel = GrpcChannel.ForAddress("http://localhost:5102");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
Console.WriteLine("Server says: " + reply.Message);

//service 2: metrics used separately
channel = GrpcChannel.ForAddress("http://localhost:5258");
var metricsClient = new NameMetrics.NameMetricsClient(channel);
var lengthReply = await metricsClient.GetLengthAsync(new LengthRequest { Name = name });
Console.WriteLine($"Your name has {lengthReply.Length} characters.");
