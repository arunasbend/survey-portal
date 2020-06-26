import React from 'react';

const QuestionArea = props => (
  <div className="take-survey">
    <div className="take-survey__head">{props.title}</div>
    <div className="take-survey__content">
      {props.children}
    </div>
  </div>);

QuestionArea.propTypes = {
  children: React.PropTypes.element.isRequired,
  title: React.PropTypes.string.isRequired,
};

export default QuestionArea;
