namespace GBBulletin.Models
{
    public class Newsinfo
    {
        public int NewsinfoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ArticlePicture { get; set; }
        public string Newsarticle { get; set; }
        public string? Author { get; set; }
        public int? NewsGenreId { get; set; }
        public virtual NewsGenre? NewsGenre { get; set; }
        public bool? TrendingNow { get; set; }
        public bool? BreakingNews { get; set; }
        public bool? IsPublished { get; set; }
        public bool? PickOfMonth { get; set; }
        public bool? EditorsPick { get; set; }
        public DateOnly? DatePublished { get; set; }
        public int? ReadingDuration { get; set; }
        public bool? IsApproved { get; set; }
    } 
}