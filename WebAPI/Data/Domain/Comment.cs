namespace WebAPI.Data.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        
        public string Statement { get; set; }

        public double? ToneScore { get; set; }
        
        public string Tone { get; set; }
    }
}
