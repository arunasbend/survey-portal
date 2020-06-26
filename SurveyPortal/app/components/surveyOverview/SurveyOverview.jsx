import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import React from 'react';
import actionCreators from '../../actions';


import Header from './Header';
import SurveyManager from './SurveyManager';
// import LinearDiagram from './LinearDiagram';
// import CircleDiagram from './CircleDiagram';

class SurveyOverview extends React.Component {
  componentDidMount() {
    this.props.getSurveyOverview();
  }

  render() {
    return (
      <div>
        <Header header={this.props.survey.header} />
        <SurveyManager surveyManager={this.props.survey.surveyManager} />
        {/* <div className="module-dashboard">
          <LinearDiagram linearDiagram={this.props.survey.linearDiagram} />
          <CircleDiagram circleDiagram={this.props.survey.circleDiagram} />
        </div> */}
      </div>
    );
  }
}

SurveyOverview.propTypes = {
  survey: React.PropTypes.shape({
    header: React.PropTypes.shape({
      statisticsData: React.PropTypes.arrayOf(React.PropTypes.shape({
        title: React.PropTypes.string,
        icon: React.PropTypes.string,
        count: React.PropTypes.number,
      })),
      surveyData: React.PropTypes.arrayOf(React.PropTypes.shape({
        title: React.PropTypes.string,
        class: React.PropTypes.string,
        count: React.PropTypes.number,
      })),
    }),
    linearDiagram: React.PropTypes.arrayOf(React.PropTypes.shape({
      title: React.PropTypes.string,
      amount: React.PropTypes.number,
    })),
    circleDiagram: React.PropTypes.arrayOf(React.PropTypes.shape({
      title: React.PropTypes.string,
      amount: React.PropTypes.number,
      id: React.PropTypes.number,
      color: React.PropTypes.string,
    })),
    surveyManager: React.PropTypes.arrayOf(React.PropTypes.shape({
      name: React.PropTypes.string,
      title: React.PropTypes.string,
      class: React.PropTypes.string,
    })),
  }).isRequired,
  getSurveyOverview: React.PropTypes.func.isRequired,
};

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);
const mapStateToProps = state => ({
  survey: state.surveyOverview.survey,
});

export default connect(mapStateToProps, mapDispatchToProps)(SurveyOverview);
