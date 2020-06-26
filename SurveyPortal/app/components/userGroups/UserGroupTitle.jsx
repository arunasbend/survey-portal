import React from 'react';

const UserGroupTitle = props => (
  <div className="user-groups__title clearfix">
    {props.children}
    <a
      href="#title"
      className="btn-action btn-action--position-right"
      onClick={props.onClick}
    >
      {props.buttonName}
    </a>
  </div>
);

UserGroupTitle.propTypes = {
  buttonName: React.PropTypes.string.isRequired,
  children: React.PropTypes.element.isRequired,
  onClick: React.PropTypes.func.isRequired,
};

export default UserGroupTitle;
