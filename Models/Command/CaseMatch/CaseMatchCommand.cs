

namespace MatchCaseService.Models.Command.CaseMatch
{
    using MediatR;
    using System.ComponentModel.DataAnnotations;
    public class CaseMatchCommand: IRequest<CaseMatchCommandResult>
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(1_000_000)]
        public string Description { get; set; } = null!;
    }
}

