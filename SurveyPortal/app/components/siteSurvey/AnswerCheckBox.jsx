import React from 'react';

const AnswerCheckBox = props => (
  <div className="checkbox-wrapper">
    <label htmlFor={`checkbox${props.id}`}>
      <input
        id={`checkbox${props.id}`}
        type="checkbox"
        checked={props.checked}
        onChange={e => props.handleCheckChange(e, props.questionIndex, props.id)}
        value={props.title}
      />
      <span className="checkbox-text">{props.title}</span>
    </label>
  </div>
);

AnswerCheckBox.propTypes = {
  checked: React.PropTypes.bool.isRequired,
  handleCheckChange: React.PropTypes.func.isRequired,
  title: React.PropTypes.string.isRequired,
  questionIndex: React.PropTypes.number.isRequired,
  id: React.PropTypes.number.isRequired,
};

export default AnswerCheckBox;
