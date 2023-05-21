using Movie_Catalog_REST_API.Dto;
using Movie_Catalog_REST_API.Models;

namespace Movie_Catalog_REST_API.Services
{
    public interface IMovieService
    {
        void AddMovie(MovieDTO movieToAdd);
        MovieDTO GetLastAddedMovie();
        IEnumerable<MovieDTO> GetMoviesByYear(int year);
        IEnumerable<MovieDTO> GetMoviesByGenre(string genre);
    }
}
