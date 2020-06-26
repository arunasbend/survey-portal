import React from 'react';
import shortId from 'shortid';

import UserGroupCell from './UserGroupCell';

const UserGroupCells = (props) => {
  const renderUserGroupCells = () =>
   (props.userGroups.map(group => (
     <UserGroupCell
       groupName={group.groupName}
       peopleCount={group.peopleCount}
       key={shortId.generate()}
       id={group.id}
     />
  )));

  return (
    <td className="content__table--result-groups">
      {renderUserGroupCells()}
    </td>
  );
};

UserGroupCells.propTypes = {
  userGroups: React.PropTypes.arrayOf(React.PropTypes.shape({
    groupName: React.PropTypes.string.isRequired,
    peopleCount: React.PropTypes.number.isRequired,
  })).isRequired,
};


export default UserGroupCells;
