import React from 'react';
import { Field, reduxForm } from 'redux-form';

const renderField = ({ input, id, label, type, meta: { touched, error } }) => (
  <div className="survey-form__group">
    <label htmlFor={id}>{label}</label>
    <div>
      <input {...input} placeholder={label} type={type} />
    </div>
    {touched && error && <span style={{ color: 'red' }}>{error}</span>}
  </div>
);

const UserPasswordRecoveryChangePasswordForm = (props) => {
  const renderErrorMessage = () => (
    <div className="errorMessage">
      {props.errorMessage}
    </div>
  );
  return (
    <form onSubmit={props.handleSubmit}>
      <div>
        <div className="survey-login">
          <div className="survey-login__form-wrapper">
            <h1 className="heading1">Survey Login</h1>
            <fieldset className="survey-form">
              <div className="survey-login__form-wrapper">
                <Field
                  label="Email"
                  component={renderField}
                  name="email"
                  id="sf-input-email"
                  type="text"
                />
                <Field
                  label="Password"
                  component={renderField}
                  name="password"
                  id="sf-input-pwd"
                  type="password"
                />
                <Field
                  label="Confirm Password"
                  component={renderField}
                  type="password"
                  name="confirmPassword"
                  id="sf-input-pwdconfirm"
                />
              </div>
              {props.errorMessage ? renderErrorMessage() : null}
            </fieldset>
          </div>
        </div>
        <div className="survey-login">
          <div className="survey-bottom-nav">
            <div className="survey-bottom-nav--left">
              <button
                style={{ width: '100px' }}
                type="submit"
                className="btn-action btn-action--type-submit"
              > Submit
            </button>
            </div>
          </div>
        </div>
      </div>
    </form>
  );
};

UserPasswordRecoveryChangePasswordForm.propTypes = {
  errorMessage: React.PropTypes.string.isRequired,
  handleSubmit: React.PropTypes.func.isRequired,
};

renderField.propTypes = {
  input: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  id: React.PropTypes.string.isRequired,
  label: React.PropTypes.string.isRequired,
  type: React.PropTypes.string.isRequired,
  meta: React.PropTypes.shape({
    error: React.PropTypes.string,
    touched: React.PropTypes.bool,
  }).isRequired,
};

export default reduxForm({
  form: 'userPasswordRecovery',
})(UserPasswordRecoveryChangePasswordForm);
