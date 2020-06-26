import React from 'react';
import { connect } from 'react-redux';

const UserGroupToolTip = (props) => {
  const renderUsers = () => {
    const usersThatBelongToTheGroup =
     props.users.filter(user => user.groupId === props.id);
    return (
      usersThatBelongToTheGroup.map(user => (
        <li className="tooltip__user" key={user.id}>{user.name}</li>
        ),
      )
    );
  };

  const users = renderUsers();
  if (users.length === 0) {
    return null;
  }
  return (
    <div className="tooltip__content">
      <div className="tooltip">
        <div className="tooltip__arrow" />
        <ul>
          {users}
        </ul>
      </div>
    </div>
  );
};

UserGroupToolTip.propTypes = {
  users: React.PropTypes.arrayOf(React.PropTypes.shape({
    name: React.PropTypes.string,
    group: React.PropTypes.string,
    id: React.PropTypes.number,
  })).isRequired,

};

const mapStateToProps = state => ({
  users: state.users.users,
});

export default connect(mapStateToProps, {})(UserGroupToolTip);
