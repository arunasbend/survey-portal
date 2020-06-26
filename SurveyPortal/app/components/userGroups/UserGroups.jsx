import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import actionCreators from '../../actions';

import UserGroupSection from './UserGroupSection';
import UserGroupTitle from './UserGroupTitle';
import GroupItem from './GroupItem';
import UserItem from './UserItem';
import UserGroupContent from './UserGroupContent';

class UserGroups extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      groupName: '',
      userName: '',
      userGroup: props.groups[0].title,
    };

    props.setVisibilityFilter(props.groups[0].title);

    this.renderSelectOptions = this.renderSelectOptions.bind(this);
    this.handeGroupChange = this.handeGroupChange.bind(this);
    this.handleGroupNameChange = this.handleGroupNameChange.bind(this);
    this.handleUserNameChange = this.handleUserNameChange.bind(this);
    this.renderGroups = this.renderGroups.bind(this);
    this.renderUsers = this.renderUsers.bind(this);
    this.addGroup = this.addGroup.bind(this);
    this.addUser = this.addUser.bind(this);
    this.removeGroup = this.removeGroup.bind(this);
    this.removeUser = this.removeUser.bind(this);
    this.onRenameGroup = this.onRenameGroup.bind(this);
  }
  onRenameGroup(id, title) {
    this.props.renameGroup(id, title);
  }

  addGroup() {
    this.props.addGroup(this.state.groupName);
    this.setState({
      groupName: '',
    });
  }

  addUser() {
    this.props.addUser(this.state.userName, this.state.userGroup);
    this.setState({
      userName: '',
    });
  }
  removeGroup(id) {
    this.props.removeGroup(id);
  }

  removeUser(id) {
    this.props.removeUser(id);
  }

  handeGroupChange(e) {
    this.setState({
      userGroup: e.target.value,
    });
    this.props.setVisibilityFilter(e.target.value);
  }

  handleGroupNameChange(e) {
    this.setState({
      groupName: e.target.value,
    });
  }

  handleUserNameChange(e) {
    this.setState({
      userName: e.target.value,
    });
  }

  renderGroups() {
    return (
    this.props.groups.map(group => (
      <GroupItem
        key={group.id}
        members={group.members}
        surveys={group.surveys}
        title={group.title}
        removeGroup={this.removeGroup}
        id={group.id}
        onRename={this.onRenameGroup}
      />
    ))
    );
  }

  renderUsers() {
    return (
      this.props.users.map((user) => {
        if (this.props.visibilityFilter === user.group) {
          return (
            <UserItem
              key={user.id}
              name={user.name}
              group={user.group}
              removeUser={this.removeUser}
              id={user.id}
            />
          );
        }
        return null;
      })
    );
  }

  renderSelectOptions() {
    return this.props.groups.map(group => (
      <option key={group.id}>{group.title}</option>),
    );
  }

  render() {
    return (
      <div className="survey-content">
        <UserGroupSection>
          <UserGroupTitle
            buttonName="Add Group"
            onClick={this.addGroup}
          >
            <div className="user-groups__input-container">
              <div className="user-groups__input">
                <input
                  type="text"
                  className="input-field--autocomplete"
                  placeholder="Group name"
                  autoComplete="on"
                  value={this.state.groupName}
                  onChange={this.handleGroupNameChange}
                />
              </div>
            </div>
          </UserGroupTitle>
          <UserGroupContent>
            <tr>
              <th>Group</th>
              <th>Members</th>
              <th>Surveys</th>
            </tr>
            {this.renderGroups()}
          </UserGroupContent>
        </UserGroupSection>
        <UserGroupSection>
          <UserGroupTitle
            buttonName="Add User"
            onClick={this.addUser}
          >
            <div className="user-groups__input-container">
              <div className="user-groups__input">
                <input
                  type="text"
                  className="input-field--autocomplete"
                  placeholder="User name or email"
                  autoComplete="on"
                  value={this.state.userName}
                  onChange={this.handleUserNameChange}
                />
              </div>
              <div className="user-groups__select">
                <select
                  id="user-rights"
                  value={this.state.userGroup}
                  onChange={this.handeGroupChange}
                >
                  {this.renderSelectOptions()}
                </select>
              </div>
            </div>
          </UserGroupTitle>
          <UserGroupContent>
            <tr>
              <th>User Name</th>
              <th>Group Name</th>
            </tr>
            {this.renderUsers()}
          </UserGroupContent>
        </UserGroupSection>
      </div>
    );
  }
}

UserGroups.propTypes = {
  groups: React.PropTypes.arrayOf(
    React.PropTypes.shape({
      title: React.PropTypes.string.isRequired,
      id: React.PropTypes.number.isRequired,
      members: React.PropTypes.number.isRequired,
      surveys: React.PropTypes.number.isRequired,
    })).isRequired,
  users: React.PropTypes.arrayOf(
    React.PropTypes.shape({
      name: React.PropTypes.string.isRequired,
      id: React.PropTypes.number.isRequired,
      group: React.PropTypes.string.isRequired,
    })).isRequired,
  renameGroup: React.PropTypes.func.isRequired,
  removeUser: React.PropTypes.func.isRequired,
  removeGroup: React.PropTypes.func.isRequired,
  addUser: React.PropTypes.func.isRequired,
  addGroup: React.PropTypes.func.isRequired,
  setVisibilityFilter: React.PropTypes.func.isRequired,
  visibilityFilter: React.PropTypes.string.isRequired,
};

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);
const mapStateToProps = state => ({
  visibilityFilter: state.users.visibilityFilter,
  groups: state.groups.groups,
  users: state.users.users,
});

export default connect(mapStateToProps, mapDispatchToProps)(UserGroups);
