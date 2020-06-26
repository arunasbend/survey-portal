/* eslint-disable react/no-children-prop */
import React from 'react';
import { Route, Link } from 'react-router-dom';

const NavLink = ({ label, to }) => (
  <Route
    path={to}
    children={({ match }) => (
      <li className={match ? 'active' : ''}>
        <Link to={to}> {label}</Link>
      </li>
  )}
  />
);

NavLink.propTypes = {
  label: React.PropTypes.string.isRequired,
  to: React.PropTypes.string.isRequired,
};

export default NavLink;
