using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Movie_Catalog_REST_API.Dto;
using Movie_Catalog_REST_API.Models;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text.Json;

namespace Movie_Catalog_REST_API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        public MovieService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void AddMovie(MovieDTO movieToAdd)
        {
            Movie movieModel = _mapper.Map<Movie>(movieToAdd);
            movieModel.Id = movies.Max(r => r.Id) + 1;
          
            movies.Add(movieModel);
        }
        public MovieDTO GetLastAddedMovie()
        {
            IEnumerable<Movie> movieListByAddDate = movies.OrderByDescending(movie => movie.TimeCreated.Date).ThenByDescending(movie => movie.TimeCreated.TimeOfDay);
            Movie LatestAddedMovie = movieListByAddDate.First();
            if (DateTimeIsUnique(movieListByAddDate, LatestAddedMovie.TimeCreated))
            {
                MovieDTO LatestAddedMovieDTO = _mapper.Map<MovieDTO>(LatestAddedMovie);
                return LatestAddedMovieDTO;
            }
            else //sorting by Id, when there are more records added exactly at the same time
            {
                MovieDTO LatestAddedMovieByIdDTO = _mapper.Map<MovieDTO>(movieListByAddDate.OrderByDescending(movie => movie.Id).First());
                return LatestAddedMovieByIdDTO;
            }
        }
        private static bool DateTimeIsUnique(IEnumerable<Movie> moviesList, DateTime latestAddDate)
        {
            List<Movie> list = new List<Movie>();
           
            foreach (var movie in moviesList)
            {
                if (movie.TimeCreated == latestAddDate)          
                    list.Add(movie);
                else
                    break;
            }

            if (list.Count != 1)
                return false;

            return true;            
        }
        public IEnumerable<MovieDTO> GetMoviesByGenre(string genre)
        {
            IEnumerable<MovieDTO> result = _mapper.Map<IEnumerable<MovieDTO>>(movies.FindAll(movie => movie.Genre.Contains(genre)));
            return result;
        }
        public IEnumerable<MovieDTO> GetMoviesByYear(int year)
        {
            IEnumerable<MovieDTO> result = _mapper.Map<IEnumerable<MovieDTO>>(movies.Where(movie => movie.WorldPremiereDate.Year == year));
            return result;
        }
        
        private static readonly List<Movie> movies = new List<Movie>
        {
            new Movie {Id=1, Title="The Shawshank Redemption", Genre={"Drama", "Crime"}, WorldPremiereDate=new DateTime(1994,10,10), Director={"Frank Darabont"}, Duration=142},
            new Movie {Id=2, Title="The Intouchables", Genre={"Biography", "Comedy", "Drama"}, WorldPremiereDate=new DateTime(2011,10,23), Director={"Olivier Nakache", "Éric Toledano"}, Duration=112},
            new Movie {Id=3, Title="The Godfather", Genre={"Drama"}, WorldPremiereDate=new DateTime(1972,03,14), Director={"Francis Ford Coppola"}, Duration=175},
            new Movie {Id=4, Title="Shrek", Genre={"Animation", "Adventure", "Comedy"}, WorldPremiereDate=new DateTime(2001,04,22), Director={ "Andrew Adamson", "Vicky Jenson"}, Duration=90 },
            new Movie {Id=5, Title="Ratatouille", Genre={"Animation", "Comedy", "Family"}, WorldPremiereDate=new DateTime(2007,06,22), Director={"Brad Bird"}, Duration=111},
            new Movie {Id=6, Title="God Bless America", Genre={"Comedy", "Crime"}, WorldPremiereDate=new DateTime(2011,09,09), Director={"Bobcat Goldthwait"}, Duration=105}
        };
    }
}
