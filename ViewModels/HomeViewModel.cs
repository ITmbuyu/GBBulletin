using GBBulletin.Models;

namespace GBBulletin.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Newsinfo> Newsinfos { get; set; }
        public IEnumerable<NewsGenre> NewsGenres { get; set; }
        public List<Newsinfo> BreakingNews { get;  set; }
        public List<Newsinfo> TrendingNow { get;  set; }
        public List<Newsinfo> PickOfMonth { get; set; }
        public List<IGrouping<string, Newsinfo>> NewsByGenre { get; set; }
        public List<Newsinfo> EditorsPick { get; set; }
    }
}
