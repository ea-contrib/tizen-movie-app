import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";

import { ReduxState } from "../../Store";
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
        <div>Home</div>
        {this.props.fetchedMovies[0] && (
          <div onClick={() => this.props.getMovie(this.props.fetchedMovies[0].id)}>
            <div>{this.props.fetchedMovies[0].name}</div>
            <img src={this.props.fetchedMovies[0].posterUrl} />
          </div>
        )}
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
