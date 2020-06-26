import React from 'react';
import { Field, reduxForm } from 'redux-form';
import { Link } from 'react-router-dom';
import validate from './registrationValidation';

const renderField = ({ input, id, label, type, meta: { touched, error } }) => (
  <div className="survey-form__group">
    <label htmlFor={id}>{label}</label>
    <div>
      <input {...input} placeholder={label} type={type} />
    </div>
    {touched && error && <span style={{ color: 'red' }}>{error}</span>}
  </div>
);

const RegistrationForm = (props) => {
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
            <h1 className="heading1">Survey Sign Up</h1>
            <fieldset className="survey-form">
              <Field
                label="Email"
                component={renderField}
                name="email"
                id="sf-input-email"
                type="text"
              />
              <Field
                label="User Name"
                component={renderField}
                name="username"
                id="sf-input-username"
                type="text"
              />
              <Field
                label="First Name"
                component={renderField}
                name="firstname"
                id="sf-input-firstname"
                type="text"
              />
              <Field
                label="Last Name"
                component={renderField}
                name="lastname"
                id="sf-input-lastname"
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
            </fieldset>
            {props.errorMessage ? renderErrorMessage() : null}
            <div className="survey-form__group">
              <Link to="/login">Login</Link>
              <Link to="/password-recovery">Forgot password?</Link>

            </div>
          </div>
        </div>
        <div className="survey-login">
          <div className="survey-bottom-nav">
            <div className="survey-bottom-nav--left">
              <button
                type="submit"
                className="btn-action btn-action--type-submit"
                style={{ width: '100px' }}
              >
              Submit
              </button>
            </div>
          </div>
        </div>
      </div>
    </form>
  );
};

RegistrationForm.propTypes = {
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
  form: 'userRegistration',
  validate,
})(RegistrationForm);
