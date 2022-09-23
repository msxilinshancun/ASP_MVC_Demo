using MatchCaseService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MatchCaseService.Models.Command.CaseMatch;
using Newtonsoft.Json;

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
        public async Task<IActionResult> CaseMatch()
        {
            string title = HttpContext.Request.Form["title"];
            string description = HttpContext.Request.Form["description"];
            
            #region call python
            string path = System.Environment.CurrentDirectory + @"\Controllers\main.py";
            string sArguments = path;
            sArguments += " " + title + " " + description;
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";//cmd is full path to python.exe
            start.Arguments = sArguments;//args is path to .py file and any cmd line args
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using(Process process = Process.Start(start))
            {
                using(StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
            
            #endregion
            
            #region deal python json file
            StreamReader r = new StreamReader(Environment.CurrentDirectory + @"\Controllers\res1.json");
            string jsonString = r.ReadToEnd();
            Dictionary<string,List<string>>? m = JsonConvert.DeserializeObject<Dictionary<string,List<string>>>(jsonString);
            List<string> titles = m["titles"];
            List<string> ids = m["ids"];
            #endregion
            ViewData["Title"] = title;
            ViewData["Description"] = description;
            ViewData["Name"] = "Xu Kang";
            ViewData["Date"] =  DateTime.Now.ToString();
            ViewData["Related_ids"] = ids;
            ViewData["Related_titles"] = titles;
            return View();
        }
        
        [HttpGet]
        [Route("Home/GetCaseMatchView")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetCaseMatchView()
        {
            return View();
        }
        
        
        
        [HttpGet]
        [Route("Home/GetCaseById/{id}")]
        public async Task<IActionResult> GetCaseById([FromRoute] string id)
        {
            CaseData caseData = new CaseData();
            Dictionary<string,string> dic = caseData.GetCaseById(id);
            ViewData["Id"] = id;
            ViewData["Title"] = dic["Title"];
            ViewData["Description"] = dic["Description"];
            ViewData["Comments"] = dic["Comments"];
            ViewData["Assignee"] = dic["Assignee"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
