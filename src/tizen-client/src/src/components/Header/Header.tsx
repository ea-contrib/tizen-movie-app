import * as React from "react";
import { Link } from "react-router-dom";

import "./Header.styl";
import "./HeaderCategories.styl";
import "./HeaderSearch.styl";
import "./HeaderTime.styl";

interface Props {
  authenticated: boolean;
}
interface State {}

export class Header extends React.Component<Props, State> {
  render() {
    return (
      <header className="header">
        <div className="header__menu-wrapper">
          <ul className="categories-menu">
            <li className="categories-menu__item">
              <Link to="/">Home</Link>
            </li>
            <li className="categories-menu__item">
              <Link to="/about">About</Link>
            </li>
            {!this.props.authenticated && (
              <li className="categories-menu__item categories-menu__item--active">
                <Link to="/authorization">Authorization</Link>
              </li>
            )}
            {this.props.authenticated && (
              <li className="categories-menu__item">
                <Link to="/profile">Profile</Link>
              </li>
            )}
          </ul>
        </div>

        <div className="header__search-wrapper">
          <div className="search-box">
            <input
              type="text"
              className="search-box__input"
              id="search-box-id"
              placeholder=" "
            />
            <label htmlFor="search-box-id" className="search-box__label">
              Search...
            </label>
          </div>
        </div>

        <div className="header__time-wrapper">
          <div className="header__time-wrapper">
            <div className="time-info">
              <div className="time-info__time">21:12pm</div>
              <div className="time-info__day-of-week">Friday</div>
              <div className="time-info__date">Jun 11, 2020</div>
            </div>
          </div>
        </div>
      </header>
    );
  }
}
