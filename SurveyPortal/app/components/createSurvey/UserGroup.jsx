import React from 'react';

const UserGroup = (props) => {
  const isActive = `btn-status btn-status--green ${props.selected ? 'active' : ''}`;
  return (
    <input
      type="button"
      value={props.title}
      onClick={() => props.onToggle(props.id)}
      className={isActive}
    />
  );
};

UserGroup.propTypes = {
  title: React.PropTypes.string.isRequired,
  selected: React.PropTypes.bool.isRequired,
  id: React.PropTypes.number.isRequired,
  onToggle: React.PropTypes.func.isRequired,
};

export default UserGroup;
