import React from 'react';
import shortId from 'shortid';

import SurveyLine from './SurveyLine';

const SurveyManager = (props) => {
  const resolveClassName = (status) => {
    switch (status) {
      case 'Disabled':
        return 'btn-status btn-status--red';
      case 'Published':
        return 'btn-status btn-status--blue';
      case 'Completed':
        return 'btn-status btn-status--green';
      default:
        return 'btn-status btn-status--navy';
    }
  };

  const surveyManager = props.surveyManager;
  const tableItems = surveyManager.map(survey => (
    <SurveyLine
      key={shortId.generate()}
      className={resolveClassName(survey.status)}
      status={survey.status}
      id={survey.id}
      name={survey.name}
    />
  ));
  return (
    <div className="page-content">
      <table className="page-content__table">
        <tbody>
          <tr>
            <th className="content__table--status">Status</th>
            <th className="content__table--name">Name</th>
            <th className="content__table--buttons">&nbsp;</th>
          </tr>
          {tableItems}
        </tbody>
      </table>
    </div>
  );
};

SurveyManager.propTypes = {
  surveyManager: React.PropTypes.arrayOf(React.PropTypes.shape({
    name: React.PropTypes.string,
    title: React.PropTypes.string,
    class: React.PropTypes.string,
  })).isRequired,
};

export default SurveyManager;
