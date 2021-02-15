namespace WebAPI.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class CommentAddRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string comment { get; set; }
    }
}
