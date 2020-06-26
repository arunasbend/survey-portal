import React from 'react';
import { connect } from 'react-redux';

export default (ComposedComponent) => {
  class Authentication extends React.Component {
    componentWillMount() {
      if (!this.props.isAuthenticated) {
        this.props.history.push('/login');
      }
    }

    componentWillUpdate(nextProps) {
      if (!nextProps.isAuthenticated) {
        nextProps.history.push('/login');
      }
    }

    render() {
      return <ComposedComponent {...this.props} />;
    }
  }

  const mapStateToProps = state => ({
    isAuthenticated: state.auth.authenticated,
  });

  Authentication.propTypes = {
    isAuthenticated: React.PropTypes.bool.isRequired,
    history: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  };

  return connect(mapStateToProps)(Authentication);
};
