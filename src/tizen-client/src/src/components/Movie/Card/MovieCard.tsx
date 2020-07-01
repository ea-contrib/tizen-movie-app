import * as React from "react";
import { MoviePoster } from "./MovieCardPoster";

import { Movie } from "../../../models";

import "./MovieCard.styl";

interface Props {
  movie: Movie;

  onClick: (movie: Movie) => void;
}
interface State {}

export class MovieCard extends React.Component<Props, State> {
  render() {
    return (
      <div tabIndex={0} className="movie" onClick={() => this.props.onClick(this.props.movie)}>
        <div className="movie__poster">
          <MoviePoster url={this.props.movie.posterUrl} />
        </div>
        <div className="movie__description-wrapper">
          <div className="movie-description__name">{this.props.movie.name}</div>
          <div className="movie-description__year">{this.props.movie.year}</div>
          <div className="movie-description__rating">{this.props.movie.rating}</div>
        </div>
      </div>
    );
  }
}
