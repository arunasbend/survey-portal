import React from 'react';
import shortId from 'shortid';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import actionCreators from '../../actions';

import QuestionArea from './QuestionArea';
import AnswerCheckBox from './AnswerCheckBox';
import AnswerRadio from './AnswerRadio';
import TextArea from './TextArea';
import RangeSelector from './RangeSelector';

import '../../styles/survey.css';

class Survey extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      questions: this.props.questions,
    };

    this.onSurveySubmit = this.onSurveySubmit.bind(this);
    this.setQuestionState = this.setQuestionState.bind(this);

    this.renderComponent = this.renderComponent.bind(this);
    this.renderHeading = this.renderHeading.bind(this);
    this.renderRadioAnswers = this.renderRadioAnswers.bind(this);
    this.renderCheckBoxAnswers = this.renderCheckBoxAnswers.bind(this);
    this.renderTextAreaQuestion = this.renderTextAreaQuestion.bind(this);
    this.renderRangeQuestion = this.renderRangeQuestion.bind(this);

    this.handleOptionChange = this.handleOptionChange.bind(this);
    this.handleCheckChange = this.handleCheckChange.bind(this);
    this.handleTextAreaContentChange = this.handleTextAreaContentChange.bind(this);
    this.handleRangeSelectorRadioValue = this.handleRangeSelectorRadioValue.bind(this);
  }

  componentWillMount() {
    this.props.loadSurveyById(this.props.match.params.id);
  }

  componentWillReceiveProps(nextProps) {
    this.state = {
      questions: nextProps.questions.map((question) => {
        if (question.type === 0 || question.type === 1) {
          return question.answers.map(answer =>
            ({
              id: answer.id,
              checked: false,
            }),
          );
        } else if (question.type === 2) {
          return question.answers[0].answer;
        } else if (question.type === 3) {
          return '';
        }
        return null;
      }),
    };
  }

  onSurveySubmit() {
    const surveySubmission = {
      id: this.props.surveyId,
      questions: this.state.questions.map((question, i) =>
        ({
          id: this.props.questions[i].id,
          title: this.props.questions[i].title,
          type: this.props.questions[i].type,
          data: this.state.questions[i],
        }),
      ),
    };
    this.props.submitSurveyQuestions(surveySubmission, this.props.history);
  }

  setQuestionState(question, questionId) {
    this.setState({
      questions: [
        ...this.state.questions.slice(0, questionId),
        question,
        ...this.state.questions.slice(parseInt(questionId, 10) + 1),
      ],
    });
  }

  handleOptionChange(e, questionId, toggleId) {
    const question = this.state.questions[questionId].map((item, i) => {
      if (i === toggleId) {
        return {
          checked: true,
          id: item.id,
        };
      }
      return {
        checked: false,
        id: item.id,
      };
    });
    this.setQuestionState(question, questionId);
  }

  handleCheckChange(e, questionId, toggleId) {
    const question = this.state.questions[questionId];
    question[toggleId] = {
      id: question[toggleId].id,
      checked: !question[toggleId].checked,
    };
    this.setQuestionState(question, questionId);
  }

  handleTextAreaContentChange(e, questionId) {
    this.setQuestionState(e.target.value, questionId);
  }

  handleRangeSelectorRadioValue(e, questionId, number) {
    this.setQuestionState(number.toString(), questionId);
  }

  renderRadioAnswers(index) {
    const rows = this.props.questions[index].answers.map((answer, i) => (
      <AnswerRadio
        key={shortId.generate()}
        title={answer.answer}
        handleOptionChange={this.handleOptionChange}
        checked={this.state.questions[index][i].checked}
        id={i}
        questionIndex={index}
      />
    ));
    return (
      <div id="radio-button-list" className="take-survey-list">
        {rows}
      </div>
    );
  }

  renderCheckBoxAnswers(index) {
    const rows = this.props.questions[index].answers.map((answer, i) => (
      <AnswerCheckBox
        key={shortId.generate()}
        title={answer.answer}
        checked={this.state.questions[index][i].checked}
        handleCheckChange={this.handleCheckChange}
        id={i}
        questionIndex={index}
      />
    ));
    return (
      <div id="checkbox-list" className="take-survey-list">
        {rows}
      </div>
    );
  }

  renderTextAreaQuestion(index) {
    return (
      <TextArea
        handleTextAreaContentChange={this.handleTextAreaContentChange}
        value={this.state.questions[index]}
        id={index}
      />
    );
  }

  renderRangeQuestion(index) {
    const selectorData = this.props.questions[index].answers[0].answer.split('|');
    return (
      <RangeSelector
        data={selectorData}
        active={this.state.questions[index]}
        handleRangeSelectorRadioValue={this.handleRangeSelectorRadioValue}
        id={index}
      />
    );
  }

  renderHeading() {
    return (
      <div className="survey-content">
        <h1 className="main-headline">{this.props.title}</h1>
        <div className="survey-content__intro">
          <p>{this.props.description}</p>
          <div className="survey-content__intro-icons">
            <i className="fa fa-calendar fa-border fa-pull-left" />
            <span>Due date:<br />
              {this.props.dueDate}</span>
          </div>
        </div>
      </div>
    );
  }

  renderComponent() {
    const questions = this.props.questions.map((item, index) => {
      const question = () => {
        if (item.type === 0) {
          return this.renderCheckBoxAnswers(index);
        } else if (item.type === 1) {
          return this.renderRadioAnswers(index);
        } else if (item.type === 2) {
          return this.renderTextAreaQuestion(index);
        } else if (item.type === 3) {
          return this.renderRangeQuestion(index);
        }
        return null;
      };
      return (
        <QuestionArea
          title={this.props.questions[index].title}
          key={this.props.questions[index].id}
        >
          {question()}
        </QuestionArea>
      );
    });
    return (
      <div>
        {this.renderHeading()}
        <div className="survey-content">
          {questions}
          <div className="survey-bottom-nav">
            <input
              onClick={this.onSurveySubmit}
              type="button" value="Submit"
              className="btn-action btn-action--type-submit"
            />
          </div>
        </div>
      </div>
    );
  }

  render() {
    return this.renderComponent();
  }
}

Survey.propTypes = {
  loadSurveyById: React.PropTypes.func.isRequired,
  questions: React.PropTypes.arrayOf(React.PropTypes.object).isRequired,
  submitSurveyQuestions: React.PropTypes.func.isRequired,
  title: React.PropTypes.string.isRequired,
  description: React.PropTypes.string.isRequired,
  dueDate: React.PropTypes.string.isRequired,
  history: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  surveyId: React.PropTypes.string.isRequired,
  match: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
};

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);
const mapStateToProps = state => ({
  questions: state.surveyCompletion.questions,
  dueDate: state.surveyCompletion.dueDate,
  description: state.surveyCompletion.description,
  title: state.surveyCompletion.title,
  userId: state.user.userData.id,
  surveyId: state.surveyCompletion.id,
  loadSurveyById: state.loadSurvey,
  loading: state.surveyCompletion.loading,
});

export default connect(mapStateToProps, mapDispatchToProps)(Survey);
