using GBBulletin.Models;

namespace GBBulletin.ViewModels
{
    public class NewsDetailsViewModel
    {
        //add a property to hold the current news details
        public Newsinfo Newsinfo { get; set; }
        public IEnumerable<NewsGenre> NewsGenres { get; set; }

    }
}
