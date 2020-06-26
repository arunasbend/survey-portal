import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import actionCreators from '../../actions';
import UserPasswordRecoveryChangePasswordForm from './UserPasswordRecoveryChangePasswordForm';

const UserPasswordRecoveryChangePassword = (props) => {
  const handleSubmit = (values) => {
    props.changeUserPassword(values.email, values.password, props.match.params.id, props.history);
  };
  return (
    <UserPasswordRecoveryChangePasswordForm
      onSubmit={handleSubmit}
      errorMessage={props.errorMessage}
    />
  );
};
const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);

UserPasswordRecoveryChangePassword.propTypes = {
  errorMessage: React.PropTypes.string.isRequired,
  history: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  changeUserPassword: React.PropTypes.func.isRequired,
  match: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
};

const mapStateToProps = state => ({
  errorMessage: state.error.forgotpassword,
});

export default connect(mapStateToProps, mapDispatchToProps)(UserPasswordRecoveryChangePassword);
