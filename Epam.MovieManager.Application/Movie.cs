using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.MovieManager.Application
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(int movieId, string title, string genre, int releasedYear, string director)
        {
            MovieId = movieId;
            Title = title;
            Genre = genre;
            ReleasedYear = releasedYear;
            Director = director;
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleasedYear { get; set; }
        public string Director { get; set; }
        
        
    }
}
