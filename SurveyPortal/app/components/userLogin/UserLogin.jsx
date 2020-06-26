import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import actionCreators from '../../actions';

class UserLogin extends React.Component {
  constructor(props) {
    super(props);
    this.state =
    {
      username: '',
      password: '',
    };
    this.onChange = this.onChange.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
    this.renderErrorMessage = this.renderErrorMessage.bind(this);
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

  onChange(e) {
    this.setState({ [e.target.name]: e.target.value });
  }

  onSubmit(e) {
    e.preventDefault();
    this.props.loginUser(this.state.username, this.state.password);
  }

  renderErrorMessage() {
    return (
      <div className="errorMessage">
        {this.props.errorMessage}
      </div>
    );
  }

  render() {
    return (
      <div>
        <div className="survey-login">
          <div className="survey-login__form-wrapper">
            <h1 className="heading1">Survey Login</h1>
            <fieldset className="survey-form">
              <div className="survey-form__group">
                <label htmlFor="sf-input-firstname">User Name</label>
                <input
                  value={this.state.username}
                  onChange={this.onChange}
                  name="username"
                  id="sf-input-firstname"
                  type="text"
                />
              </div>
              <div className="survey-form__group">
                <label htmlFor="sf-input-pwd">Password</label>
                <input
                  value={this.state.password}
                  onChange={this.onChange}
                  name="password"
                  id="sf-input-pwd"
                  type="password"
                />
              </div>
              {this.props.errorMessage ? this.renderErrorMessage() : null}
              <div className="survey-form__group">
                <Link to="/signup">Sign Up</Link>
                <Link to="/password-recovery">Forgot password?</Link>

              </div>
            </fieldset>
          </div>
        </div>
        <div className="survey-login">
          <div className="survey-bottom-nav">
            <div className="survey-bottom-nav--left">
              <input
                type="button"
                value="Sign In"
                className="btn-action btn-action--type-submit"
                onClick={this.onSubmit}
              />
            </div>
          </div>
        </div>
      </div>
    );
  }
}

UserLogin.propTypes = {
  errorMessage: React.PropTypes.string.isRequired,
  loginUser: React.PropTypes.func.isRequired,
  isAuthenticated: React.PropTypes.bool.isRequired,
  history: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
};

const mapStateToProps = state => ({
  isAuthenticated: state.auth.authenticated,
  errorMessage: state.error.errorMessage,
});

export default connect(mapStateToProps, actionCreators)(UserLogin);
