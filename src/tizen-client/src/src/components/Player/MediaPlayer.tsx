import * as React from "react";

import {
  EnSubtitlesButton,
  FastForwardButton,
  PauseButton,
  PlayButton,
  PlayNextButton,
  PlayPrevButton,
  RewindButton,
  RuSubtitlesButton,
} from "./PlayerButtons";
import { Timeline } from "./Timeline";

import "./MediaPlayer.styl";

type PlayerProps = {
  videoUrl: string;
};
type PlayerState = {
  isPlaying: boolean;
  progress: number;
};

export class MediaPlayer extends React.Component<PlayerProps, PlayerState> {
  private defaultPlayerStep = 15;

  private playerRef = React.createRef<HTMLVideoElement>();

  state = {
    isPlaying: true,
    progress: 0,
  };

  componentDidMount(): void {
    // await document.documentElement.requestFullscreen();
  }

  onPlayToggleClick = async () => {
    if (this.playerRef.current) {
      if (this.state.isPlaying) {
        await this.playerRef.current.pause();
        this.setState({
          isPlaying: false,
        });
      } else {
        await this.playerRef.current.play();
        this.setState({
          isPlaying: true,
        });
      }
    }
  };

  onPlay = async () => {
    await this.onPlayToggleClick();
  };
  onPause = async () => {
    await this.onPlayToggleClick();
    if (this.playerRef.current) {
      console.log(this.playerRef.current.currentTime);
      console.log(this.playerRef.current.duration);
    }
  };

  onPlayNext = () => {};
  onPlayPrev = () => {};
  onRewind = () => {
    if (this.playerRef.current) {
      this.playerRef.current.currentTime -= this.defaultPlayerStep;
    }
  };
  onFastForward = () => {
    if (this.playerRef.current) {
      this.playerRef.current.currentTime += this.defaultPlayerStep;
    }
  };
  onEnSubtitlesToggle = () => {};
  onRuSubtitlesToggle = () => {};
  onProgress = () => {
    this.setState({
      ...this.state,
      progress: this.getCurrentProgress(),
    });
  };

  getCurrentProgress() {
    if (this.playerRef.current) {
      return (
        this.playerRef.current.currentTime / this.playerRef.current.duration
      );
    }

    return 0;
  }

  render() {
    return (
      <div className="player__container">
        <div className="player__video-container">
          <video
            src={this.props.videoUrl}
            autoPlay={true}
            className="player"
            ref={this.playerRef}
            onClick={this.onPlayToggleClick}
            tabIndex={0}
            onProgress={this.onProgress}
          />
        </div>

        <div className="player__action-bar">
          <div className="player__timeline-container">
            <Timeline progressPercentage={this.state.progress * 100} />
          </div>
          <div className="player__actions-container">
            <div className="player__actions-prev">
              <PlayPrevButton onClick={this.onPlayPrev} />
            </div>
            <div className="player__actions-rewind">
              <RewindButton onClick={this.onRewind} />
            </div>
            <div className="player__actions-play-toggle">
              {this.state.isPlaying ? (
                <PauseButton onClick={this.onPause} />
              ) : (
                <PlayButton onClick={this.onPlay} />
              )}
            </div>
            <div className="player__actions-fast-forward">
              <FastForwardButton onClick={this.onFastForward} />
            </div>
            <div className="player__actions-next">
              <PlayNextButton onClick={this.onPlayNext} />
            </div>
            <div className="player__actions-en-subtitles-toggle">
              <EnSubtitlesButton
                onClick={this.onEnSubtitlesToggle}
                isActive={false}
              />
            </div>
            <div className="player__actions-ru-subtitles-toggle">
              <RuSubtitlesButton
                onClick={this.onRuSubtitlesToggle}
                isActive={false}
              />
            </div>
          </div>
        </div>
      </div>
    );
  }
}
