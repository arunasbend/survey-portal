import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import actionCreators from '../../actions';
import RegistrationForm from './RegistrationForm';

class UserRegister extends React.Component {

  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }
  componentWillMount() {
    if (this.props.isAuthenticated) {
      this.props.history.push('/');
    }
  }
  componentDidUpdate() {
    if (this.props.isAuthenticated) {
      this.props.history.push('/');
    }
  }
  handleSubmit(values) {
    this.props.registerUser(values);
  }
  render() {
    return (
      <RegistrationForm onSubmit={this.handleSubmit} errorMessage={this.props.errorMessage} />
    );
  }
  }

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);

const mapStateToProps = state => ({
  isAuthenticated: state.auth.authenticated,
  errorMessage: state.error.errorMessage,
});

UserRegister.propTypes = {
  errorMessage: React.PropTypes.string.isRequired,
  history: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  isAuthenticated: React.PropTypes.bool.isRequired,
  registerUser: React.PropTypes.func.isRequired,
};

export default connect(mapStateToProps, mapDispatchToProps)(UserRegister);
