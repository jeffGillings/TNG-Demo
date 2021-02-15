namespace WebAPI.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class CommentUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Tone { get; set; }
    }
}
