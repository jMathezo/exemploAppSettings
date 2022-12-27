using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace ExemploAppSettings.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MinhasConfigs _minhasConfigs;
        private readonly IOptions<MinhasConfigs> _minhasConfigsOptions;

        public HomeController(IConfiguration configuration,
            IOptions<MinhasConfigs> minhasConfigsOptions)
        {
            _configuration = configuration;
            // Binding
            _minhasConfigs = new MinhasConfigs();
            configuration.GetSection("MinhasConfigs").Bind(_minhasConfigs);

            _minhasConfigsOptions = minhasConfigsOptions;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var ex1 = _configuration["Boleano"];
            var ex1Aninhado = _configuration["MinhasConfigs:PropCompartilhada"];

            var ex2 = _configuration.GetValue<bool>("MinhasConfigs:DemaisConfigs:ParamBoleano");
            // aqui vamos tentar obter um valor inexistente justamente para vermos o valor default
            var ex2ValorDefault = _configuration.GetValue<string>("MinhasConfigs", "valor não encontrado");

            // 
            var ex3 = _configuration.GetSection("MinhasConfigs");
            // Fazendo binding da section para a classe MinhasConfigs
            var ex3Binding = _configuration.GetSection("MinhasConfigs").Get<MinhasConfigs>();
            // Aqui podemos navegar entre as sessões e buscar um valor especifico
            var ex3ValorEspecifico = _configuration.GetSection("MinhasConfigs").
                             GetSection("DemaisConfigs").
                             GetSection("ParamBoleano").Value;
            // Outra forma de buscar o valor agora usando o GetValue<T>
            var ex3GetValue = _configuration.GetSection("MinhasConfigs").
                            GetSection("DemaisConfigs").
                            GetValue<bool>("ParamBoleano");
            // Outra forma de buscar o valor agora em linha
            var ex3GetSectionInLine = _configuration.GetSection("MinhasConfigs:DemaisConfigs:ParamBoleano").Value;

            var ex4Binding = _minhasConfigs.PropCompartilhada;

            var _minhasConfigsOptionsvariavel = _minhasConfigsOptions.Value?.DemaisConfigs;

            return Ok(new
            {
                //
                ex1,
                ex1Aninhado,
                ex2,
                ex2ValorDefault,
                ex3 = ex3["DemaisConfigs:Param1"],
                ex3Binding = ex3Binding.PropReescrita,
                ex3ValorEspecifico,
                ex3GetValue,
                ex3GetSectionInLine,
                ex4Binding
            });
        }
    }
}
