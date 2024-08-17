using Microsoft.AspNetCore.Mvc;

namespace Practice_web_api
{
    public class ProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public new string? Type { get; set; }
        public new int? Status { get; set; }
        public new string? Title { get; set; }
        public new string? Detail { get; set; }
        public new string? Instance { get; set; }

        public List<ValidationProblemDetails> Errors { get; set; }
    }
}
