namespace MatchCaseService.Models;

public class CaseModel
{
    public Dictionary<string,string> Title{ get; set; }
    public Dictionary<string,string> Incidentid{ get; set; }
    public Dictionary<string,string> Description{ get; set; }
    public Dictionary<string,string> Assignee{ get; set; }
    public Dictionary<string,string> Comments{ get; set; }
}