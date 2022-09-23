using Newtonsoft.Json;

namespace MatchCaseService.Models
{
    public class CaseData
    {
        public CaseModel CaseSQL;

        public CaseData()
        {
            var path = System.Environment.CurrentDirectory;
            StreamReader r = new StreamReader(path + @"\Models\cases.json");
            string jsonString = r.ReadToEnd();
            CaseModel? m = JsonConvert.DeserializeObject<CaseModel>(jsonString);
            this.CaseSQL = m;
        }

        public Dictionary<string,string> GetCaseById(string id)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            foreach (var x in this.CaseSQL.Title)
            {
                if (x.Key == id)
                {
                    ret.Add("Title", x.Value);
                    break;
                }
            }

            
            foreach (var x in CaseSQL.Description)
            {
                if (x.Key == id)
                {
                    ret.Add("Description", x.Value);
                    break;
                }
            }
            foreach (var x in CaseSQL.Comments)
            {
                if (x.Key == id)
                {
                    ret.Add("Comments", x.Value);
                    break;
                }
            }
            foreach (var x in CaseSQL.Assignee)
            {
                if (x.Key == id)
                {
                    ret.Add("Assignee", x.Value);
                    break;
                }
            }
            
            return ret;
        }
    }
}

