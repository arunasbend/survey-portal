import React from 'react';
import { connect } from 'react-redux';
import actionCreators from '../../actions';
import Notification from './Notification';

const Notifications = (props) => {
  const closeNotification = (id) => {
    props.dismissNotification(id);
  };

  const renderedNotifications = props.notifications.map(notification => (
    <Notification
      key={notification.id}
      id={notification.id}
      message={notification.message}
      type={notification.kind}
      onCloseClick={closeNotification}
    />
  ),
  );
  return (
    <div className="notif__container" >
      {renderedNotifications}
    </div>
  );
};

Notifications.propTypes = {
  notifications: React.PropTypes.arrayOf(React.PropTypes.shape({
    message: React.PropTypes.string,
    id: React.PropTypes.number,
    timeout: React.PropTypes.number,
  })).isRequired,
};

const mapStateToProps = state => ({
  notifications: state.notifications,
});

export default connect(mapStateToProps, actionCreators)(Notifications);
