import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import actionCreators from '../../actions';

class UserPasswordRecovery extends React.Component {
  constructor() {
    super();
    this.state = {
      email: '',
    };
    this.onSendEmail = this.onSendEmail.bind(this);
    this.onInputChange = this.onInputChange.bind(this);
  }

  onInputChange(e) {
    this.setState({
      email: e.target.value,
    });
  }

  onSendEmail() {
    this.props.sendEmail(this.state.email);
  }

  render() {
    return (
      <div>
        <div className="survey-login">
          <div className="survey-login__form-wrapper">
            <h1 className="heading1">Forgot password?</h1>
            <p>
                Enter the email address you use to sign in,
                and we&apos;ll send you an email with instructions to reset your password.
            </p>
            <fieldset className="survey-form">
              <div className="survey-form__group">
                <label htmlFor="sf-input-email">Email Adress</label>
                <input
                  id="sf-input-email"
                  type="text"
                  value={this.state.email}
                  onChange={this.onInputChange}
                />
              </div>
            </fieldset>
          </div>
        </div>
        <div className="survey-login">
          <div className="survey-bottom-nav">
            <div className="survey-bottom-nav--left">
              <input
                type="button"
                value="Send email"
                className="btn-action btn-action--type-submit"
                onClick={this.onSendEmail}
              />
            </div>
          </div>
        </div>
      </div>
    );
  }
}

UserPasswordRecovery.propTypes = {
  sendEmail: React.PropTypes.func.isRequired,
};

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);

export default connect(undefined, mapDispatchToProps)(UserPasswordRecovery);
