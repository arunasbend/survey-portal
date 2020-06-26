import React from 'react';
import NavBar from './NavBar';
import Notifications from './Notifications';

const App = props => (
  <div className="page-frame">
    <NavBar />
    <div className="page-frame__inner">
      {props.children}
      <Notifications />
    </div>
  </div>
);

App.propTypes = {
  children: React.PropTypes.arrayOf(React.PropTypes.element).isRequired,
};

export default App;
