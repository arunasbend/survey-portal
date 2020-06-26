import * as group from './group';
import * as user from './user';
import * as createSurvey from './surveyCreation';
import * as submitSurveyQuestions from './surveySubmission';
import * as error from './error';
import * as survey from './survey';
import * as notification from './notification';

export default {
  ...group,
  ...user,
  ...createSurvey,
  ...submitSurveyQuestions,
  ...error,
  ...survey,
  ...notification,
};
