import * as React from "react";
import { connect } from "react-redux";
import { Dispatch } from "redux"
import { withRouter, RouteComponentProps } from "react-router-dom";

import { ReduxState } from "../../Store";
import { MoviesList } from "../../components";
import { fetchMovies, Action as MoviesAction } from "../../services/movies";
import { Movie, MoviesFilter } from "../../models";

interface PathParams {}
interface Props extends RouteComponentProps<PathParams> {
  fetchedMovies: Array<Movie>;
  moviesFilter: MoviesFilter;

  fetchMovies: (filter: MoviesFilter) => void;
  openMovie: (movie: Movie) => void;
}
interface State {}

class Home extends React.Component<Props, State> {
  componentDidMount() {
    this.props.fetchMovies(this.props.moviesFilter);
  }

  render() {
    return (
      <>
        <div className="main__filter-wrapper"></div>

        <div className="main__movies-wrapper">
          <MoviesList
            movies={this.props.fetchedMovies}
            onItemClick={this.props.openMovie}
          />
        </div>
      </>
    );
  }
}

const mapStateToProps = (state: ReduxState, props: RouteComponentProps) => ({
  fetchedMovies: state.data.movies.allFetched.movies,
  moviesFilter: state.data.movies.allFetched.filter,
});

const mapDispatchToProps = (dispatch: Dispatch<MoviesAction>, props: RouteComponentProps) => ({
  fetchMovies: (filter: MoviesFilter) => fetchMovies(filter)(dispatch),

  openMovie: (movie: Movie) => props.history.push("/movies/" + movie.id),
});

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Home));
