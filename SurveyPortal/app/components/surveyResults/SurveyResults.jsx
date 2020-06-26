import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import actionCreators from '../../actions';

import Heading from './Heading';
import Questions from './Questions';

class SurveyResults extends React.Component {
  componentDidMount() {
    this.props.loadSurveyResults(this.props.match.params.id);
  }

  render() {
    if (!this.props.loading) {
      return (
        <div className="survey-content">
          <Heading heading={this.props.heading} />
          <Questions questions={this.props.questions} />
        </div>
      );
    } else if (this.props.error !== '') {
      return (
        <div className="survey-content">
          <div className="errorMessage">{this.props.error}</div>
        </div>
      );
    }
    return (
      <div className="survey-content">
        <p>Loading</p>
      </div>
    );
  }
}

SurveyResults.propTypes = {
  error: React.PropTypes.string.isRequired,
  loading: React.PropTypes.bool.isRequired,
  loadSurveyResults: React.PropTypes.func.isRequired,
  heading: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  questions: React.PropTypes.array.isRequired, // eslint-disable-line react/forbid-prop-types
  match: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
};

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);

const mapStateToProps = state => ({
  loading: state.surveyResults.loading,
  heading: state.surveyResults.heading,
  questions: state.surveyResults.questions,
  error: state.error.errorMessage,
});

export default connect(mapStateToProps, mapDispatchToProps)(SurveyResults);
