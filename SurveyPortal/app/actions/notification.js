import { SEND_NOTIFICATION, DISMISS_NOTIFICATION, CLEAR_NOTIFICATIONS } from './constants';

export const sendNotification = (notification) => {
  const data = { ...notification };
  if (!data.id) {
    data.id = new Date().getTime();
  }
  return (dispatch) => {
    dispatch({ type: SEND_NOTIFICATION, data });
    if (data.dismissAfter) {
      setTimeout(() => {
        dispatch({
          type: DISMISS_NOTIFICATION,
          data: data.id,
        });
      }, data.dismissAfter);
    }
  };
};

export const dismissNotification = id => (
    { type: DISMISS_NOTIFICATION, data: id }
);

export function clearNotifications() {
  return { type: CLEAR_NOTIFICATIONS };
}
