using MatchCaseService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MatchCaseService.Models.Command.CaseMatch;

namespace MatchCaseService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Home/CaseMatch")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseMatchCommandResult))]
        public async Task<IActionResult> CaseMatch([FromBody] CaseMatchCommand command, [FromHeader] string tenantId)
        {
            Console.WriteLine(command.Title);
            Console.WriteLine(command.Description,"- - ");
            string[] Relateds = new string[3];
            Relateds[0] = command.Title;
            Relateds[1] = command.Title + 'a';
            Relateds[2] = command.Title + 'b';
            CaseMatchCommandResult caseResult = new CaseMatchCommandResult()
            {
                Title =command.Title,
                Description = command.Description,
                Name = "Xu Kang",
                Date = DateTime.Now.ToString(),
                Related = Relateds
            };
            return View();
        }
        
        [HttpGet]
        [Route("Home/CaseMatch")]
        [Consumes("application/json")]
        public async Task<IActionResult> CaseMatch()
        {
            return View();
        }
        
        public async Task<IActionResult> GetCaseMatchView()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}