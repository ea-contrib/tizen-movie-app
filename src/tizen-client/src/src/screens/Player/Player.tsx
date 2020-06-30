import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";

import { ReduxState } from "../../Store";
import { Movie } from "../../models";
import { MediaPlayer } from "../../components";

interface PathParams {
  id: string;
}
interface Props extends RouteComponentProps<PathParams> {
  currentMovie: Movie | null;
}
interface State {}

class Player extends React.Component<Props, State> {
  render() {
    console.log(this.props.match.params.id);

    if (this.props.currentMovie && this.props.currentMovie.videoUrl) {
      return <MediaPlayer videoUrl={this.props.currentMovie.videoUrl} />;
    }
    return null;
  }
}

const mapStateToProps = (state: ReduxState) => ({
  currentMovie: state.data.movies.movieDetails.openedMovie,
});

const mapDispatchToProps = {};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(Player));
