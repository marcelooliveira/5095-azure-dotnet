using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace VollMed.FunctionApp;

public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function(nameof(Function1))]
    public async Task Run(
        [ServiceBusTrigger("vollmedqueue", Connection = "")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}

public class ConsultaQueueMessage
{
    public int MedicoId { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
}

public class ConsultaPorMedico
{
    public long MedicoId { get; set; }
    public string MedicoNome { get; set; }
    public DateTime Data { get; set; }
    public int QtdeConsultas { get; set; }
    public decimal Honorarios { get; set; }
}

public record ResultadoMensal
(
    string id,
    long medicoId,
    string medicoNome,
    int ano,
    int mes,
    int qtdeConsultas,
    decimal honorarios
);
