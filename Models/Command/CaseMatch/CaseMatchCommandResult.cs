namespace MatchCaseService.Models.Command.CaseMatch
{
    public class CaseMatchCommandResult
    {
        public string? Title { get; init; }
        
        public string? Description { get; init; }
        
        public string? Name{ get; init; }
        
        public string? Date{ get; init; }
        
        public string[]? Related{ get; init; }
    }
}

