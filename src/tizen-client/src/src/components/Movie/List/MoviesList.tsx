import * as React from "react";

import { MovieCard } from "../../index";

import { Movie } from "../../../models";

import "./MoviesList.styl";

interface Props {
  movies: Array<Movie>;

  onItemClick: (movie: Movie) => void;
}
interface State {}

export class MoviesList extends React.Component<Props, State> {
  render() {
    return (
      <div className="movies">
        {this.props.movies.map((movie) => (
          <div key={movie.id} className="movies__item">
            <div className="movies__item-wrapper">
              <MovieCard
                movie={movie}
                onClick={this.props.onItemClick}
              />
            </div>
          </div>
        ))}
      </div>
    );
  }
}
