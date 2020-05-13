import { Movie, MoviesFilter } from "../../models";

const inceptionMovie: Movie = {
  id: "352a04b8-b5ce-4b0b-95b8-9c9548e77583",
  name: "Inception",
  year: 2010,
  posterUrl:
    "https://upload.wikimedia.org/wikipedia/en/2/2e/Inception_%282010%29_theatrical_poster.jpg",
  rating: 8.7,
};

export const fetchMovies = (filter: MoviesFilter): Promise<Array<Movie>> => {
  const movies = new Promise<Array<Movie>>((resolve, reject) => {
    setTimeout(() => {
      resolve([inceptionMovie]);
    }, 250);
  });
  return movies;
};

export const getMovie = (id: string): Promise<Movie> => {
  return new Promise<Movie>((resolve, reject) => {
    setTimeout(() => {
      resolve(inceptionMovie);
    }, 250);
  });
};
