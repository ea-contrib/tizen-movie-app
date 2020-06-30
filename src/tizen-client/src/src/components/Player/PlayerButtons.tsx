import * as React from "react";

import PlayIcon from "./icons/Play.svg";
import PauseIcon from "./icons/Pause.svg";
import PlayNextIcon from "./icons/PlayNext.svg";
import FastForwardIcon from "./icons/FastForward.svg";

import "./PlayerButtons.styl";

export interface PlayerButtonProps {
  onClick: () => void;
}
export interface SubtitleButtonProps {
  onClick: () => void;
  isActive: boolean;
}

export const PlayButton = (props: PlayerButtonProps) => {
  return (
    <div className="play-button" onClick={props.onClick} tabIndex={0}>
      <PlayIcon />
    </div>
  );
};

export const PauseButton = (props: PlayerButtonProps) => {
  return (
    <div className="pause-button" onClick={props.onClick} tabIndex={0}>
      <PauseIcon />
    </div>
  );
};

export const PlayNextButton = (props: PlayerButtonProps) => {
  return (
    <div className="play-next-button" onClick={props.onClick} tabIndex={0}>
      <PlayNextIcon />
    </div>
  );
};
export const PlayPrevButton = (props: PlayerButtonProps) => {
  return (
    <div className="play-prev-button" onClick={props.onClick} tabIndex={0}>
      <PlayNextIcon />
    </div>
  );
};

export const FastForwardButton = (props: PlayerButtonProps) => {
  return (
    <div className="fast-forward-button" onClick={props.onClick} tabIndex={0}>
      <FastForwardIcon />
    </div>
  );
};

export const RewindButton = (props: PlayerButtonProps) => {
  return (
    <div className="rewind-button" onClick={props.onClick} tabIndex={0}>
      <FastForwardIcon />
    </div>
  );
};
export const EnSubtitlesButton = (props: SubtitleButtonProps) => {
  let activeClass = props.isActive ? "subtitles-button--active" : "";
  return (
    <div
      className={`subtitles-button__en ${activeClass}`}
      onClick={props.onClick}
      tabIndex={0}
    >
      EN
    </div>
  );
};

export const RuSubtitlesButton = (props: SubtitleButtonProps) => {
  let activeClass = props.isActive ? "subtitles-button--active" : "";
  return (
    <div
      className={`subtitles-button__ru ${activeClass}`}
      onClick={props.onClick}
      tabIndex={0}
    >
      RU
    </div>
  );
};
