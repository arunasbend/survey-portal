/* eslint-disable react/no-array-index-key */
/* eslint-disable no-param-reassign */
import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FieldArray } from 'redux-form';
import actionCreators from '../../actions';

import SurveyContentIntro from './SurveyContentIntro';
import Question from './Question';
import QuestionsForm from './QuestionsForm';

import '../../styles/site-styles.css';

class CreateSurvey extends React.Component {
  constructor(props) {
    super(props);
    this.renderQuestions = this.renderQuestions.bind(this);
    this.onToggle = this.onToggle.bind(this);
    this.submit = this.submit.bind(this);

    this.state = {
      userGroups: this.props.userGroups,
    };
    console.log(this);
  }

  onToggle(id) {
    this.setState({
      userGroups: this.state.userGroups.map((item) => {
        if (item.id === id) return { ...item, selected: !item.selected };
        return item;
      }),
    });
  }

  submit(values) {
    values.userGroups = this.state.userGroups;
    if (values.questions == null) {
      this.props.showCreateSurveyError();
    } else {
      this.props.createSurvey(values, this.props.history);
    }
  }

  renderQuestions({ fields }) {
    const addQuestion = () => {
      fields.push({
        check: [
          null,
        ],
        radio: [
          null,
        ],
        type: 'check',
      });
    };
    const onRemoveQuestion = (index) => {
      fields.remove(index);
    };
    return (
      <div>
        {fields.map((question, index) => (
          <Question
            key={index}
            number={index}
            onRemoveQuestion={onRemoveQuestion}
            id={index}
            onTabChange={this.onTabChange}
            field={question}
            question={question}
          />
          ),
        )}
        <input
          type="button" value="Add Question" onClick={addQuestion}
          className="btn-action btn-action--type-submit"
        />
      </div>
    );
  }

  render() {
    return (
      <div className="survey-content">
        <QuestionsForm onSubmit={this.submit}>
          <SurveyContentIntro
            userGroups={this.state.userGroups}
            onToggle={this.onToggle}
            surveyTitle="New Survey"
          />
          <FieldArray name="questions" component={this.renderQuestions} />
        </QuestionsForm>
      </div>
    );
  }
}

CreateSurvey.propTypes = {
  userGroups: React.PropTypes.arrayOf(React.PropTypes.shape({
    title: React.PropTypes.string,
    id: React.PropTypes.number,
    selected: React.PropTypes.bool,
  })).isRequired,
  createSurvey: React.PropTypes.func.isRequired,
  showCreateSurveyError: React.PropTypes.func.isRequired,
  history: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
};

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);
const mapStateToProps = state => ({
  userGroups: state.createSurveyFirst.data.userGroups,
  questions: state.createSurveyFirst.questions,
});

export default connect(mapStateToProps, mapDispatchToProps)(CreateSurvey);
