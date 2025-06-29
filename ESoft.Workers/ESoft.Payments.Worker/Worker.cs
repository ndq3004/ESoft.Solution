using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Framing;

namespace ESoft.Payments.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        //private readonly ServiceBusClient _serviceBusClient;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            //string serviceBusCnn = _configuration.GetConnectionString("ServiceBus") ?? "";
            //_serviceBusClient = new ServiceBusClient(serviceBusCnn);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            List<Task> processors = new List<Task>();
            //Solution 1:
            foreach (var name in new[] { "Processor1", "Processor2", "Processor3" }) // assume that they are containers
            {
                var processor = CreateProcessor(name, stoppingToken);
                processors.Add(processor.StartProcessingAsync(stoppingToken));
            }

            // Wait for all processors to start
            await Task.WhenAll(processors);
            _logger.LogInformation("All processors started successfully.");

            await Task.Delay(Timeout.Infinite, stoppingToken); // Keep the worker running until cancellation is requested




            // Solution 2:
            //var receiver = _serviceBusClient.CreateReceiver("payment");

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    if (_logger.IsEnabled(LogLevel.Information))
            //    {
            //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    }
            //    //await ReceiveMessageAsync(receiver);



            //    await Task.Delay(1000, stoppingToken);
            //}
        }

        private ServiceBusProcessor CreateProcessor(string name, CancellationToken stoppingToken)
        {
            var serviceBusCnn = _configuration.GetConnectionString("ServiceBus") ?? "";
            ServiceBusClient serviceBusClient = new ServiceBusClient(serviceBusCnn);

            var processor = serviceBusClient.CreateProcessor("payment", new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 3,
                AutoCompleteMessages = false,
                Identifier = name,
            });


            processor.ProcessMessageAsync += async args =>
            {
                try
                {
                    string body = args.Message.Body.ToString();
                    _logger.LogInformation("{client} has Received message: {messageBody} in {partitionKey} at {time}", name, body, args.Message.PartitionKey ?? "NoKey", DateTime.UtcNow);
                    await Task.Delay(2000, stoppingToken);
                    // Process the message here
                    // ...
                    // Complete the message so that it is not received again.
                    await args.CompleteMessageAsync(args.Message, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message");
                    await args.AbandonMessageAsync(args.Message);
                }
            };

            processor.ProcessErrorAsync += async args =>
            {
                _logger.LogError(args.Exception, "Error in message processing: {errorMessage} at {time}", args.Exception.Message, DateTime.UtcNow);
                // Handle the error (e.g., log it, send to a dead-letter queue, etc.)
            };

            _logger.LogInformation("Processor {name} created!", name);

            return processor;
        }

        private async Task ReceiveMessageAsync(ServiceBusReceiver receiver, CancellationToken stoppingToken)
        {
            try
            {
                ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();
                if (message != null)
                {
                    string body = message.Body.ToString();
                    _logger.LogInformation("Received message: {messageBody}", body);
                    // Process the message here
                    // ...
                    // Complete the message so that it is not received again.
                    await receiver.CompleteMessageAsync(message, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
            }
        }
    }
}
