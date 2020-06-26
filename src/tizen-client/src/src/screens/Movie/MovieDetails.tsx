import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";

import { ReduxState } from "../../Store";
import { getMovie } from "../../services/movies";
import { Movie } from "../../models";

interface PathParams {
  id: string;
}
interface Props extends RouteComponentProps<PathParams> {
  movie: Movie | null;

  getMovie: (id: string) => void;
}
interface State {}

class MovieDetails extends React.Component<Props, State> {
  componentDidMount() {
    this.props.getMovie(this.props.match.params.id);
  }

  render() {
    return (
      <div className="main__movie-wrapper">
        <i
          className="main__button-go-back"
          onClick={() => this.props.history.goBack()}
        >
          {"<-"}
        </i>
        {this.props.movie !== null && <label>{this.props.movie.name}</label>}
      </div>
    );
  }
}

const mapStateToProps = (state: ReduxState) => ({
  movie: state.data.movies.movieDetails.openedMovie,
});

const mapDispatchToProps = {
  getMovie: getMovie,
};

export default withRouter(
  connect(mapStateToProps, mapDispatchToProps)(MovieDetails)
);
