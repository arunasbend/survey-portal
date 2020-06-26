import React from 'react';
import User from '../../resources/images/usr.jpg';

const UserItem = props => (
  <tr>
    <td className="user-groups__name">
      <div>
        <div>
          <figure className="survey-results__user-pic">
            <a href="#usr-profile"><img alt="usr" src={User} /></a>
          </figure>
          <a href="#usr-profile" className="survey-results__username">{props.name}</a>
        </div>
      </div>
    </td>
    <td>
      {props.group}
    </td>
    <td className="user-groups__buttons">
      <a href="#delte" className="btn-modify" onClick={() => props.removeUser(props.id)} >
        <i className="fa fa-close" />
        Delete
      </a>
      <a href="#edit" className="btn-modify">
        <i className="fa fa-edit" />
          Edit
      </a>
    </td>
  </tr>
);

UserItem.propTypes = {
  group: React.PropTypes.string.isRequired,
  removeUser: React.PropTypes.func.isRequired,
  id: React.PropTypes.number.isRequired,
  name: React.PropTypes.string.isRequired,
};

export default UserItem;
