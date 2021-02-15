namespace WebAPI
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WebAPI.DTO;
    using WebAPI.Services;
    using static System.Net.Mime.MediaTypeNames;
    using static Microsoft.AspNetCore.Http.StatusCodes;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<ActionResult<CommentResponse>> Get(int commentId)
        {
            Outcome<CommentResponse> outcome = await _commentService.GetCommentAsync(commentId);
            if(!outcome.Successful)
            { 
                return RequestError(outcome);
            }

            return Ok(outcome.Result);
        }

        [HttpPost]
        [Consumes(Application.Json)]
        [ProducesResponseType(Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status409Conflict)]
        public async Task<IActionResult> Post(CommentAddRequest request)
        {
            Outcome<int> outcome = await _commentService.AddCommentAsync(request);
            if(!outcome.Successful)
            {
                return RequestError(outcome);
            }

            return CreatedAtAction(nameof(Get), new { commentId = outcome.Result }, null);
        }

        [HttpPut]
        [Consumes(Application.Json)]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status400BadRequest)]
        [ApiExplorerSettings(IgnoreApi=true)]
        public async Task<ActionResult<CommentResponse>> Put(CommentUpdateRequest request)
        {
            Outcome<CommentResponse> outcome = await _commentService.UpdateCommentAsync(request);
            if(!outcome.Successful)
            {
                return RequestError(outcome);
            }

            return NoContent();
        }

        private ActionResult RequestError(Outcome outcome)
        {
            if(outcome.Reason == Status409Conflict)
            { 
                return Conflict(outcome.ToString());
            }

            return BadRequest(outcome.ToString());
        }
    }
}
