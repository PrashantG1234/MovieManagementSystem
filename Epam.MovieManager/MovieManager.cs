using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Epam.MovieManager.Application;
using log4net;

namespace Epam.MovieManager
{
    public partial class MovieManager : Form
    {
        MovieServices movieService = null;
        private static readonly ILog log = LogManager.GetLogger(typeof(MovieManager));

        public MovieManager()
        {
            InitializeComponent();
            movieService = new MovieServices();
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository);

            log.Info("Movie Manager application started.");
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                log.Info("Fetching first movie...");
                ClearAllTextboxes(this);
                Movie movie = movieService.First();
                if (movie == null)
                {
                    MessageBox.Show("No Movie Found");
                    log.Warn("No movies found in the list.");
                }
                else
                {
                    txtMovieId.Text = movie.MovieId.ToString();
                    txtTitle.Text = movie.Title;
                    txtGenre.Text = movie.Genre;
                    txtReleaseYear.Text = movie.ReleasedYear.ToString();
                    txtDirector.Text = movie.Director;
                    log.Info($"First movie retrieved: {movie.Title} ({movie.ReleasedYear})");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error fetching first movie.", ex);
            }

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMovieId.Text, out int movieId))
            {
                MessageBox.Show("No previous movie data available");
                return;
            }
            Movie movie = movieService.Next(movieId);
            if (movie == null) MessageBox.Show("Coudn't Find The Prev Movie Found");
            else
            {
                txtMovieId.Text = movie.MovieId.ToString();
                txtTitle.Text = movie.Title;
                txtGenre.Text = movie.Genre;
                txtReleaseYear.Text = movie.ReleasedYear.ToString();
                txtDirector.Text = movie.Director;

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMovieId.Text, out int movieId))
            {
                MessageBox.Show("No next movie data available");
                return;
            }
            Movie movie = movieService.Next(movieId);
            if (movie == null) MessageBox.Show("Coudn't Find The Next Movie Found");
            else
            {
                txtMovieId.Text = movie.MovieId.ToString();
                txtTitle.Text = movie.Title;
                txtGenre.Text = movie.Genre;
                txtReleaseYear.Text = movie.ReleasedYear.ToString();
                txtDirector.Text = movie.Director;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                checkAllFields();
                Movie movie = new Movie();
                if (!int.TryParse(txtMovieId.Text, out int movieId))
                {
                    MessageBox.Show("Movie Id should be a number");
                    log.Warn("Invalid Movie ID input.");
                    return;
                }

                movie.MovieId = movieId;
                movie.Title = txtTitle.Text;
                movie.Genre = txtGenre.Text;
                movie.Director = txtDirector.Text;

                if (!int.TryParse(txtReleaseYear.Text, out int releasedYear))
                {
                    MessageBox.Show("Released Year should be a number");
                    log.Warn("Invalid Release Year input.");
                    return;
                }

                movie.ReleasedYear = releasedYear;

                if (movieService.Add(movie))
                {
                    MessageBox.Show("Movie Added Successfully");
                    log.Info($"Movie added: {movie.Title} ({movie.ReleasedYear})");
                    ClearAllTextboxes(this);
                }
                else
                {
                    MessageBox.Show("Movie Already Present. Try to add a new one.");
                    log.Warn($"Attempted to add duplicate movie: {movie.Title} ({movie.ReleasedYear})");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error adding movie.", ex);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (movieService.Save()) MessageBox.Show("Movie Saved");
            else MessageBox.Show("No Movies to Save");
        }

        private void btnLoadMovies_Click(object sender, EventArgs e)
        {
            txtMovieDetails.Text = "";
            Dictionary<int,Movie> movieList = movieService.Load();
            if (movieList != null && movieList.Count!=0)
            {
                foreach (var movie in movieList.Values)
                {
                    txtMovieDetails.Text += $"Movie Id: {movie.MovieId}, Movie Title: {movie.Title}, Release Year: {movie.ReleasedYear}, Genre: {movie.Genre}, Director: {movie.Director} {Environment.NewLine} ";
                }
            }
            else
            {
                txtMovieDetails.Text = "No Movies Found";
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            checkAllFields();
            if (int.TryParse(txtMovieId.Text, out int id))
            {
                Movie movie = new Movie();
                movie.MovieId = id;
                movie.Title = txtTitle.Text;
                movie.Genre = txtGenre.Text;
                movie.Director = txtDirector.Text;
                if (int.TryParse(txtReleaseYear.Text, out int releasedYear))
                {
                    movie.ReleasedYear = releasedYear;
                }
                else
                {
                    MessageBox.Show("Released Year should be a number");
                    return;
                }
                if (movieService.Update(id, movie))
                {
                    MessageBox.Show("Movie Updated Successfully");
                    ClearAllTextboxes(this);
                }
                else
                {
                    MessageBox.Show("Movie Not Found");
                }
            }
            else
            {
                MessageBox.Show("Movie Id should be a number");
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMovieId.Text, out int id))
                {
                    MessageBox.Show("Movie Id should be a number");
                    log.Warn("Invalid Movie ID input for deletion.");
                    return;
                }

                if (movieService.Delete(id))
                {
                    MessageBox.Show("Movie Deleted Successfully");
                    log.Info($"Movie deleted: ID {id}");
                    btnLoadMovies_Click(sender, e);
                    ClearAllTextboxes(this);
                }
                else
                {
                    MessageBox.Show("Movie Not Found");
                    log.Warn($"Attempted to delete non-existing movie with ID: {id}");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error deleting movie.", ex);
            }
        }

        private void checkAllFields()
        {
            if (txtMovieId.Text == null || txtMovieId.Text == "")
            {
                MessageBox.Show("Movie Id is required");
                return;
            }
            if (txtTitle.Text == null || txtTitle.Text == "")
            {
                MessageBox.Show("Movie Title is required");
                return;
            }
            if (txtGenre.Text == null || txtGenre.Text == "")
            {
                MessageBox.Show("Movie Genre is required");
                return;
            }
            if (txtDirector.Text == null || txtDirector.Text == "")
            {
                MessageBox.Show("Movie Director is required");
                return;
            }
            if (txtReleaseYear.Text == null || txtReleaseYear.Text == "")
            {
                MessageBox.Show("Movie Release Year is required");
                return;
            }

        }
        private void ClearAllTextboxes(MovieManager movieManager)
        {
            foreach (Control control in movieManager.Controls)
            {
                if (control is TextBox box)
                {
                    box.Text = "";
                }
            }
        }
        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            ClearAllTextboxes(this);
            Movie movie = movieService.Last();
            if (movie == null) MessageBox.Show("No Movie Found");
            else
            {
                txtMovieId.Text = movie.MovieId.ToString();
                txtTitle.Text = movie.Title;
                txtGenre.Text = movie.Genre;
                txtReleaseYear.Text = movie.ReleasedYear.ToString();
                txtDirector.Text = movie.Director;

            }
        }
    }
}
