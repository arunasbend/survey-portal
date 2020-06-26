import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';
import { logoutUser } from '../../actions/user';

import Logo from './../../resources/images/logo.svg';
import User from './../../resources/images/usr.jpg';

import NavLink from './NavLink';


class NavBar extends React.Component {
  constructor(props) {
    super(props);
    this.onUserLogout = this.onUserLogout.bind(this);
    this.renderUserMenu = this.renderUserMenu.bind(this);
    this.renderSecondBar = this.renderSecondBar.bind(this);
  }

  onUserLogout() {
    this.props.logoutUser();
  }

  renderUserMenu() {
    if (this.props.isAuthenticated) {
      return (
        <div className="page-header__nav-user">
          <figure className="page-header__user-pic">
            <Link to="/edit"><img src={User} alt="usr" /></Link>
          </figure>
          <ul className="page-header__username">
            <li>
              <strong>
                <Link to="/edit">{this.props.username}</Link>
              </strong>
            </li>
            <li className="btn-user">
              <Link to="/login" onClick={this.onUserLogout}>Logout</Link>
            </li>
          </ul>
        </div>
      );
    }
    return null;
  }

  renderSecondBar() {
    if (this.props.isAuthenticated) {
      return (
        <div>
          <div className="page-breadcrumb">
            <div className="page-breadcrumb__content">
              <nav className="main-nav">
                <ul>
                  <NavLink to="/survey-overview" label="Survey overview" />
                  <NavLink to="/user-groups" label="User Groups" />
                </ul>
              </nav>
              <Link
                to="/create-survey"
                className="btn-action btn-action--position-right"
              >
                Create New Survey
              </Link>
            </div>
          </div>

        </div>
      );
    }
    return null;
  }

  render() {
    return (
      <div className="page-top-frame">
        <div className="page-top-bar">
          <header className="page-header">
            <figure className="page-header__logo">
              <Link to="/"><img src={Logo} alt="logo" /></Link>
            </figure>
            {this.renderUserMenu()}
          </header>
        </div>
        {this.renderSecondBar()}
      </div>
    );
  }
}

NavBar.propTypes = {
  username: React.PropTypes.string.isRequired,
  logoutUser: React.PropTypes.func.isRequired,
  isAuthenticated: React.PropTypes.bool.isRequired,
};

const mapDispatchToProps = dispatch => bindActionCreators({ logoutUser }, dispatch);
const mapStateToProps = state => ({
  isAuthenticated: state.auth.authenticated,
  username: state.user.userData.username,
});

export default connect(mapStateToProps, mapDispatchToProps)(NavBar);
