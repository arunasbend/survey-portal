import React from 'react';

import UserGroupToolTip from './UserGroupToolTip';

const UserGroupCell = props => (
  <div className="tooltip__container">
    <span className="btn-status btn-status--dark">
      {props.groupName}
      <strong>
        {props.peopleCount}
      </strong>
    </span>
    <UserGroupToolTip
      id={props.id}
    />
  </div>
);

UserGroupCell.propTypes = {
  groupName: React.PropTypes.string.isRequired,
  peopleCount: React.PropTypes.number.isRequired,
  id: React.PropTypes.number.isRequired,
};

export default UserGroupCell;
