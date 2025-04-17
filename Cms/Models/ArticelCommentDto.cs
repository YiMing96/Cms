namespace Cms.Models
{
    public class ArticelCommentDto
    {
        public long Id { get; set; }

        public string? Msg { get; set; }

        public int ArticelId { get; set; }

        public string? CreatedDate { get; set; }
    }
}
