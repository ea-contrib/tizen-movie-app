import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";

import { ReduxState } from "../../Store";
import { MoviesList } from "../../components";
import { fetchMovies, getMovie } from "../../services/movies";
import { Movie, MoviesFilter } from "../../models";

interface PathParams {}
interface Props extends RouteComponentProps<PathParams> {
  fetchedMovies: Array<Movie>;
  moviesFilter: MoviesFilter;

  fetchMovies: (filter: MoviesFilter) => void;
}
interface State {}

class Home extends React.Component<Props, State> {
  componentDidMount() {
    this.props.fetchMovies(this.props.moviesFilter);
  }

  openMovie(movie: Movie) {
    this.props.history.push("/movies/" + movie.id);
  }

  render() {
    return (
      <>
        <div className="main__filter-wrapper"></div>

        <div className="main__movies-wrapper">
          <MoviesList
            movies={this.props.fetchedMovies}
            itemClickAction={this.openMovie.bind(this)}
          />
        </div>
      </>
    );
  }
}

const mapStateToProps = (state: ReduxState) => ({
  fetchedMovies: state.data.movies.allFetched.movies,
  moviesFilter: state.data.movies.allFetched.filter,
});

const mapDispatchToProps = {
  fetchMovies: fetchMovies,
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Home));
