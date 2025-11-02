using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace VollMed.FunctionApp;

public class GerarProntuarioMedico
{
    private readonly ILogger<GerarProntuarioMedico> _logger;

    public GerarProntuarioMedico(ILogger<GerarProntuarioMedico> logger)
    {
        _logger = logger;
    }

    [Function("GerarProntuarioMedico")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}


public class Prontuario
{
    public long MedicoId { get; set; }
    public string MedicoNome { get; set; } = string.Empty;
    public string MedicoCrm { get; set; } = string.Empty;
    public int MedicoEspecialidade { get; set; }
    public DateTime ConsultaData { get; set; }
    public string ConsultaPaciente { get; set; } = string.Empty;
}

public enum Especialidade
{
    [Display(Name = "Cardiologia")] Cardiologia = 1,
    [Display(Name = "Neurocirurgia")] Neurocirurgia = 2,
    [Display(Name = "Cirurgia Geral")] CirurgiaGeral = 3,
    [Display(Name = "Pediatria")] Pediatria = 4,
    [Display(Name = "Oncologia")] Oncologia = 5,
    [Display(Name = "Diagnóstico")] Diagnostico = 6
}