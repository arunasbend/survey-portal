import { Provider } from 'react-redux';
import React from 'react';
import { render } from 'react-dom';
import {
  BrowserRouter as Router,
  Route,
} from 'react-router-dom';

import App from './components/layout/App';
import CreateSurvey from './components/createSurvey/CreateSurvey';
import UserLogin from './components/userLogin/UserLogin';
import UserRegister from './components/userRegister/UserRegister';
import UserEdit from './components/userEdit/UserEdit';
import SiteMap from './components/sitemap/SiteMap';
import Survey from './components/siteSurvey/Survey';
import UserPasswordRecovery from './components/userPasswordRecovery/UserPasswordRecovery';
import SurveyResults from './components/surveyResults/SurveyResults';
import UserGroups from './components/userGroups/UserGroups';
import RequireAuth from './components/userLogin/RequireAuth';
import SurveyOverview from './components/surveyOverview/SurveyOverview';
import EditSurvey from './components/createSurvey/EditSurvey';
import UserPasswordRecoveryChangePassword from './components/userPasswordRecoveryChangePassword/UserPasswordRecoveryChangePassword';

import './styles/notification.css';
import store from './store';
import { getUserData } from './actions/user';
import { AUTH_USER, UNAUTH_USER } from './actions/constants';

const ISLOGGEDIN = localStorage.getItem('login');

if (ISLOGGEDIN) {
  getUserData(store.dispatch);
  store.dispatch({ type: AUTH_USER });
} else {
  store.dispatch({ type: UNAUTH_USER });
}

render(
  <Provider store={store}>
    <Router>
      <App>
        <Route exact path="/" component={RequireAuth(SiteMap)} />
        <Route path="/login" component={UserLogin} />
        <Route path="/signup" component={UserRegister} />
        <Route path="/create-survey" component={RequireAuth(CreateSurvey)} />
        <Route path="/edit" component={RequireAuth(UserEdit)} />
        <Route path="/password-recovery" component={UserPasswordRecovery} />
        <Route path="/site-survey/:id/" component={RequireAuth(Survey)} />
        <Route path="/survey-results/:id/" component={RequireAuth(SurveyResults)} />
        <Route path="/user-groups" component={RequireAuth(UserGroups)} />
        <Route path="/survey-overview" component={RequireAuth(SurveyOverview)} />
        <Route path="/survey-edit/:id/" component={RequireAuth(EditSurvey)} />
        <Route path="/recovery/:id*" component={UserPasswordRecoveryChangePassword} />
      </App>
    </Router>
  </Provider>,
  document.querySelector('.app'),
);
