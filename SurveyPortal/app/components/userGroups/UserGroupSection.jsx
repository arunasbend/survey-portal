import React from 'react';

const UserGroupSection = props => (
  <div className="user-groups">
    {props.children}
  </div>
);

UserGroupSection.propTypes = {
  children: React.PropTypes.arrayOf(
    React.PropTypes.element.isRequired,
  ).isRequired,
};

export default UserGroupSection;
