import * as React from "react";
import { connect } from "react-redux";
import { Dispatch } from "redux";
import { withRouter, RouteComponentProps } from "react-router-dom";

import { MoviePoster, MoviesList } from "../../components";
import { ReduxState } from "../../Store";
import { getMovie, Action as MoviesAction } from "../../services/movies";
import { Movie } from "../../models";

import "./MovieDetails.styl";

interface PathParams {
  id: string;
}
interface Props extends RouteComponentProps<PathParams> {
  movie: Movie | null;
  relatedMovies: Array<Movie>;

  getMovie: (id: string) => void;
  openMovie: (movie: Movie) => void;
  play: (id: string) => void;
}
interface State {}

class MovieDetails extends React.Component<Props, State> {
  componentDidMount() {
    this.props.getMovie(this.props.match.params.id);
  }

  componentDidUpdate(
    prevProps: Readonly<Props>,
    prevState: Readonly<State>,
    snapshot?: any
  ): void {
    console.log(this.props.match.params.id);
  }

  render() {
    return (
      <div className="main__movie-wrapper">
        <div className="page__header">
          <div
            className="page__button button-go-back"
            onClick={() => this.props.history.goBack()}
          >
            &#8592;
          </div>
          <h2 className="header__title">Watch now</h2>
        </div>
        {this.props.movie !== null && (
          <>
            <div className="movie__board">
              <div className="movie__header">
                <h2 className="movie__title">{this.props.movie.name}</h2>
                <div className="movie__meta">
                  <div className="movie__meta-param">8.7/10</div>
                  <div className="movie__meta-param">
                    Action, Adventure, Sci-Fi
                  </div>
                </div>
              </div>
              <div className="movie__details">
                <div className="movie__poster">
                  <MoviePoster url={this.props.movie.posterUrl} />
                </div>
                <div className="movie__description">
                  <div className="movie__text">
                    A thief who steals corporate secrets through the use of
                    dream-sharing technology is given the inverse task of
                    planting an idea into the mind of a C.E.O.
                  </div>
                  <div className="movie__buttons">
                    <div
                      className="page__button "
                      onClick={() =>
                        this.props.movie !== null
                          ? this.props.play(this.props.movie.id)
                          : console.log("Cannot fetch movie")
                      }
                    >
                      Play
                    </div>
                    <div
                      className="page__button "
                      onClick={() => this.props.history.goBack()}
                    >
                      Like
                    </div>
                  </div>
                </div>
                <div className="movie__parameters">
                  <div className="movie__parameter">
                    <div className="movie__parameter-label">Directed</div>
                    <div className="movie__parameter-value">
                      Christopher Nolan
                    </div>
                  </div>
                  <div className="movie__parameter">
                    <div className="movie__parameter-label">Duration</div>
                    <div className="movie__parameter-value">2h 28min</div>
                  </div>
                  <div className="movie__parameter">
                    <div className="movie__parameter-label">Released</div>
                    <div className="movie__parameter-value">22 July 2010</div>
                  </div>
                </div>
              </div>
            </div>
            <div className="movie__related_panel-wrapper">
              <h2 className="related-movies__title">Viewers also watch</h2>
              <div className="related-movies__list-wrapper">
                <MoviesList
                  movies={this.props.relatedMovies}
                  onItemClick={this.props.openMovie}
                />
              </div>
            </div>
          </>
        )}
      </div>
    );
  }
}

const mapStateToProps = (state: ReduxState, props: RouteComponentProps) => ({
  movie: state.data.movies.movieDetails.openedMovie,
  relatedMovies: state.data.movies.allFetched.movies,
});

const mapDispatchToProps = (
  dispatch: Dispatch<MoviesAction>,
  props: RouteComponentProps
) => ({
  getMovie: (id: string) => getMovie(id)(dispatch),

  openMovie: (movie: Movie) => props.history.push("/movies/" + movie.id),
  play: (id: string) => props.history.push("/play/" + id),
});

export default withRouter(
  connect(mapStateToProps, mapDispatchToProps)(MovieDetails)
);
