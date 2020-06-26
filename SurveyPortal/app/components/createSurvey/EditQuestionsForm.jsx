/* eslint-disable no-class-assign */
import React from 'react';
import { reduxForm } from 'redux-form';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import actionCreators from '../../actions';

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

class EditQuestionsForm extends React.Component {
  componentDidMount() {
    this.props.loadSurvey(this.props.surveyId);
  }
  render() {
    return (
      <form onSubmit={this.props.handleSubmit(this.props.onSubmit)}>
        {this.props.children}
      </form>
    );
  }
}

EditQuestionsForm.propTypes = {
  handleSubmit: React.PropTypes.func.isRequired,
  onSubmit: React.PropTypes.func.isRequired,
  children: React.PropTypes.arrayOf(React.PropTypes.any).isRequired,
  loadSurvey: React.PropTypes.func.isRequired,
  surveyId: React.PropTypes.string.isRequired,
};

EditQuestionsForm = reduxForm({
  form: 'create-survey-edit',
  validate,
})(EditQuestionsForm);

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);

EditQuestionsForm = connect(
  state => ({
    initialValues: state.survey.data,
  }),
  mapDispatchToProps,
)(EditQuestionsForm);

export default EditQuestionsForm;
