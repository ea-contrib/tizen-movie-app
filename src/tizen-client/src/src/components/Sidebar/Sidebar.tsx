import * as React from "react";
import { Link } from "react-router-dom";

import "./Sidebar.styl";
import "./HeaderCategories.styl";
import "./HeaderSearch.styl";
import "./HeaderTime.styl";

interface Props {
  authenticated: boolean;
}
interface State {}

export class Sidebar extends React.Component<Props, State> {
  render() {
    return (
      <div className="sidebar">
      
        <div className="sidebar__account-wrapper">
            {!this.props.authenticated && (
              <li className="categories-menu__item categories-menu__item--active">
                <Link to="/authorization">Sign in</Link>
              </li>
            )}
            {this.props.authenticated && (
              <li className="categories-menu__item">
                <Link to="/profile">Profile</Link>
              </li>
            )}
        </div>

        <div className="sidebar__menu-wrapper">
          <ul className="categories-menu">
            <li className="categories-menu__item">
              <Link to="/">Home</Link>
            </li>
            <li className="categories-menu__item">
              <Link to="/movies">Movies</Link>
            </li>
            <li className="categories-menu__item">
              <Link to="/series">Series</Link>
            </li>
            <li className="categories-menu__item">
              <Link to="/about">About</Link>
            </li>
            
          </ul>
        </div>

        <div className="sidebar__time-wrapper">
          <div className="time-info">
            <div className="time-info__time">21:12pm</div>
            <div className="time-info__day-of-week">Friday</div>
            <div className="time-info__date">Jun 11, 2020</div>
          </div>
        </div>

      </div>
    );
  }
}
