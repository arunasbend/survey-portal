import { createStore, compose, applyMiddleware } from 'redux';
import ReduxThunk from 'redux-thunk';
import rootReducer from './reducers';

import userGroups from './data/userGroups';
import groups from './data/groups';
import users from './data/users';
import surveyOverviewModel from './data/surveyOverviewModel';

const defaultState = {
  user: {
    userData: {
      username: '',
      password: '',
      firstname: '',
      lastname: '',
      email: '',
      id: '',
    },
  },
  surveyCompletion: {
    questions: [],
    title: '',
    description: '',
    dueDate: '',
    surveyId: '',
  },
  createSurveyFirst: {
    data: {
      userGroups,
      name: '',
      date: '',
    },
  },
  users: {
    visibilityFilter: '',
    users,
  },
  groups: {
    groups,
  },
  surveyResults: {
    heading: {},
    questions: [],
    loading: true,
  },
  surveyOverview: {
    survey: surveyOverviewModel,
  },
  error: {
    forgotpassword: '',
    errorMessage: '',
  },
};

const enhancers = compose(
    window.devToolsExtension ? window.devToolsExtension() : f => f,
);

const createStoreWithMiddleware = applyMiddleware(ReduxThunk)(createStore);
const store = createStoreWithMiddleware(rootReducer, defaultState, enhancers);
export default store;
