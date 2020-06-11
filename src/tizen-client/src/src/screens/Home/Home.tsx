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
  getMovie: (id: string) => void;
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
          <MoviesList movies={this.props.fetchedMovies} />
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
  getMovie: getMovie,
};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Home));
