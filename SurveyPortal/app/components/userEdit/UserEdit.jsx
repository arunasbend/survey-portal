/* eslint no-console: ["error", { allow: ["warn", "error", "log"] }] */
import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import actionCreators from '../../actions';

import InputWrapper from './InputWrapper';
import User from '../../resources/images/usr.jpg';

class UserEdit extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      firstName: this.props.data.firstname,
      lastName: this.props.data.lastname,
      email: this.props.data.email,
      password: '',
      userName: this.props.data.username,
    });
    this.handleChange = this.handleChange.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
  }

  componentDidMount() {
    this.props.retrieveUserData();
  }

  componentWillReceiveProps(nextProps) {
    if (nextProps.data !== this.props.data) {
      this.setState({
        firstName: nextProps.data.firstname,
        lastName: nextProps.data.lastname,
        email: nextProps.data.email,
        userName: nextProps.data.username,
      });
    }
  }

  onSubmit() {
    this.props.submitUserData(this.props.data.id, this.state);
  }

  handleChange({ target: { name, value } }) {
    this.setState({ [name]: value });
  }

  renderInputs() {
    return (
      <fieldset className="survey-form">
        <InputWrapper
          title="First Name"
          value={this.state.firstName}
          label="firstname"
          name="firstName"
          type="text"
          onChange={this.handleChange}
        />
        <InputWrapper
          title="Last Name"
          value={this.state.lastName}
          label="lastname"
          name="lastName"
          type="text"
          onChange={this.handleChange}
        />
        <InputWrapper
          title="Email"
          value={this.state.email}
          label="email"
          name="email"
          type="text"
          onChange={this.handleChange}
        />
        <InputWrapper
          title="Password"
          value={this.state.password}
          label="pwd"
          type="password"
          name="password"
          onChange={this.handleChange}
        />
        <div className="survey-form__group">
          <input
            type="button"
            value="Save Profile"
            className="btn-action btn-action--type-submit"
            onClick={this.onSubmit}
          />
        </div>
      </fieldset>
    );
  }

  render() {
    return (
      <div className="survey-content">
        <h1 className="main-headline">Edit Profile</h1>
        <div className="survey-login__form-wrapper edit-profile">
          <div className="edit-profile__left-col">
            <fieldset className="survey-form">
              <div className="survey-form__group">
                <figure className="edit-profile__user-pic">
                  <a href="#img">
                    <img src={User} alt="usr" /></a>
                </figure>
                <input
                  type="button"
                  value="Change Avatar"
                  className="btn-status btn-status--green"
                />
              </div>
            </fieldset>
          </div>
          <div className="edit-profile__right-col">
            {this.renderInputs()}
          </div>
        </div>
      </div>
    );
  }
}

UserEdit.propTypes = {
  data: React.PropTypes.shape({
    firstname: React.PropTypes.string.isRequired,
    lastname: React.PropTypes.string.isRequired,
    email: React.PropTypes.string.isRequired,
    username: React.PropTypes.string.isRequired,
    id: React.PropTypes.string.isRequired,
  }).isRequired,
  retrieveUserData: React.PropTypes.func.isRequired,
  submitUserData: React.PropTypes.func.isRequired,
};
const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);

const mapStateToProps = state => ({
  data: state.user.userData,
});

export default connect(mapStateToProps, mapDispatchToProps)(UserEdit);
