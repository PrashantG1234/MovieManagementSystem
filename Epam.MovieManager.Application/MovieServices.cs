using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Newtonsoft.Json;

namespace Epam.MovieManager.Application
{
    public class MovieServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MovieServices));
        private Dictionary<int, Movie> movies;
        private readonly string filePath = "movie.json";

        public MovieServices()
        {
            movies = new Dictionary<int, Movie>();
            Load(); // Load existing data on startup
            log.Info("MovieServices initialized. Data loaded from file.");
        }

        public bool Add(Movie movie)
        {
            try
            {
                log.Info($"Attempting to add movie: {movie.MovieId} - {movie.Title}");

                if (movies.ContainsKey(movie.MovieId))
                {
                    log.Warn($"Movie with ID {movie.MovieId} already exists.");
                    return false;
                }

                movies.Add(movie.MovieId, movie);
                bool saved = Save();

                if (saved)
                    log.Info($"Movie added successfully: {movie.MovieId} - {movie.Title}");
                else
                    log.Error($"Failed to save movie after adding: {movie.MovieId} - {movie.Title}");

                return saved;
            }
            catch (Exception ex)
            {
                log.Error("Error adding movie", ex);
                return false;
            }
        }

        public bool Update(int movieId, Movie movie)
        {
            try
            {
                log.Info($"Attempting to update movie with ID: {movieId}");

                if (!movies.ContainsKey(movieId))
                {
                    log.Warn($"Movie with ID {movieId} not found.");
                    return false;
                }

                movies[movieId] = movie;
                bool saved = Save();

                if (saved)
                    log.Info($"Movie updated successfully: {movieId}");
                else
                    log.Error($"Failed to save movie after updating: {movieId}");

                return saved;
            }
            catch (Exception ex)
            {
                log.Error($"Error updating movie {movieId}", ex);
                return false;
            }
        }

        public bool Delete(int movieId)
        {
            try
            {
                log.Info($"Attempting to delete movie with ID: {movieId}");

                if (!movies.ContainsKey(movieId))
                {
                    log.Warn($"Movie with ID {movieId} not found.");
                    return false;
                }

                movies.Remove(movieId);
                bool saved = Save();

                if (saved)
                    log.Info($"Movie deleted successfully: {movieId}");
                else
                    log.Error($"Failed to save movie after deletion: {movieId}");

                return saved;
            }
            catch (Exception ex)
            {
                log.Error($"Error deleting movie {movieId}", ex);
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                log.Info("Saving movie data to file...");
                string json = JsonConvert.SerializeObject(movies, Formatting.Indented);
                File.WriteAllText(filePath, json);
                log.Info("Movie data saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error saving movies", ex);
                return false;
            }
        }

        public Dictionary<int, Movie> Load()
        {
            Dictionary<int, Movie> movieList = new Dictionary<int, Movie>();

            if (File.Exists(filePath))
            {
                try
                {
                    log.Info("Loading movie data from file...");
                    string json = File.ReadAllText(filePath);
                    movieList = JsonConvert.DeserializeObject<Dictionary<int, Movie>>(json) ?? new Dictionary<int, Movie>();

                    movies.Clear();  // Ensure existing movies are cleared
                    foreach (var kvp in movieList)
                    {
                        movies.Add(kvp.Key, kvp.Value);
                    }

                    log.Info($"Successfully loaded {movies.Count} movies from file.");
                }
                catch (Exception ex)
                {
                    log.Error("Error loading movies", ex);
                }
            }
            else
            {
                log.Warn("Movie data file not found. Starting with an empty collection.");
            }

            return movieList;
        }

        public Movie First()
        {
            if (movies.Count == 0)
            {
                log.Warn("No movies available to fetch the first movie.");
                return null;
            }

            Movie firstMovie = null;
            foreach (var movie in movies.Values)
            {
                if (firstMovie == null || movie.MovieId < firstMovie.MovieId)
                {
                    firstMovie = movie;
                }
            }

            log.Info($"First movie retrieved: {firstMovie.MovieId} - {firstMovie.Title}");
            return firstMovie;
        }

        public Movie Last()
        {
            if (movies.Count == 0)
            {
                log.Warn("No movies available to fetch the last movie.");
                return null;
            }

            Movie lastMovie = null;
            foreach (var movie in movies.Values)
            {
                if (lastMovie == null || movie.MovieId > lastMovie.MovieId)
                {
                    lastMovie = movie;
                }
            }

            log.Info($"Last movie retrieved: {lastMovie.MovieId} - {lastMovie.Title}");
            return lastMovie;
        }

        public Movie Next(int movieId)
        {
            var sortedMovies = new List<Movie>(movies.Values);
            sortedMovies.Sort((m1, m2) => m1.MovieId.CompareTo(m2.MovieId));
            int index = sortedMovies.FindIndex(m => m.MovieId == movieId);

            if (index == -1 || index >= sortedMovies.Count - 1)
            {
                log.Warn($"No next movie found for ID: {movieId}");
                return null;
            }

            log.Info($"Next movie retrieved: {sortedMovies[index + 1].MovieId} - {sortedMovies[index + 1].Title}");
            return sortedMovies[index + 1];
        }

        public Movie Previous(int movieId)
        {
            var sortedMovies = new List<Movie>(movies.Values);
            sortedMovies.Sort((m1, m2) => m1.MovieId.CompareTo(m2.MovieId));
            int index = sortedMovies.FindIndex(m => m.MovieId == movieId);

            if (index <= 0)
            {
                log.Warn($"No previous movie found for ID: {movieId}");
                return null;
            }

            log.Info($"Previous movie retrieved: {sortedMovies[index - 1].MovieId} - {sortedMovies[index - 1].Title}");
            return sortedMovies[index - 1];
        }
    }
}
