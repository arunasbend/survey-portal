import React from 'react';
import { reduxForm } from 'redux-form';

const validate = (values) => {
  const errors = { questions: [] };
  if (values.questions != null) {
    values.questions.forEach((item, index) => {
      errors.questions[index] = { radio: [], check: [] };
      if (item != null) {
        if (item.check) {
          item.check.forEach((value, innerIndex) => {
            if (!value) {
              errors.questions[index].check[innerIndex] = 'Is required';
            }
          });
        }
        if (item.radio) {
          item.radio.forEach((value, innerIndex) => {
            if (!value) {
              errors.questions[index].radio[innerIndex] = 'Is required';
            }
          });
        }
      }
    });
  }
  if (!values.date) {
    errors.date = 'Due date is required';
  }
  return errors;
};

const QuestionsForm = props => (
  <form onSubmit={props.handleSubmit}>
    {props.children}
  </form>
);

QuestionsForm.propTypes = {
  handleSubmit: React.PropTypes.func.isRequired,
  children: React.PropTypes.arrayOf(React.PropTypes.any).isRequired,
};

export default reduxForm({
  form: 'create-survey',
  validate,
})(QuestionsForm);
