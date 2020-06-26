import React from 'react';

const Notification = ({ type, onCloseClick, id, message }) => {
  const handleActionClick = (ev) => {
    ev.preventDefault();
    if (!onCloseClick) {
      return;
    }
    onCloseClick(id);
  };

  return (
    <div className={`notif notif--${type}`}>
      <div className="notif__content">
        <span className="notif__message">{message}</span>
      </div>
      <span className="notif__action">
        <button
          onClick={handleActionClick}
        >
          <i className="fa fa-times" aria-hidden="true" />
        </button>
      </span>
      <div className="notif__close" />
    </div>
  );
};

Notification.defaultProps = {
  type: 'info',
};

Notification.propTypes = {
  id: React.PropTypes.oneOfType([
    React.PropTypes.string,
    React.PropTypes.number,
  ]).isRequired,
  message: React.PropTypes.string.isRequired,
  type: React.PropTypes.oneOf([
    'success',
    'info',
    'warning',
    'danger',
  ]).isRequired,
  onCloseClick: React.PropTypes.func.isRequired,
};

export default Notification;
