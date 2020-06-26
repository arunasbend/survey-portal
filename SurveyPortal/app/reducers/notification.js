import { SEND_NOTIFICATION, DISMISS_NOTIFICATION, CLEAR_NOTIFICATIONS } from './../actions/constants';

export default (state = [], action) => {
  switch (action.type) {
    case SEND_NOTIFICATION: {
      return [action.data, ...state.filter(({ id }) => id !== action.data.id)];
    }
    case DISMISS_NOTIFICATION:
      return state.filter(notification =>
          notification.id !== action.data,
      );
    case CLEAR_NOTIFICATIONS:
      return [];
    default:
      return state;
  }
};
