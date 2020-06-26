import React from 'react';

const UserGroupContent = props => (
  <div className="user-groups__content">
    <table className="page-content__table">
      <tbody>
        {props.children}
      </tbody>
    </table>
  </div>
);

UserGroupContent.propTypes = {
  children: React.PropTypes.array.isRequired,  // eslint-disable-line react/forbid-prop-types
};

export default UserGroupContent;
