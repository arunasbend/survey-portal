import React from 'react';

const AnswerCheckBox = (props) => {
  const data = {
    type: 'radio',
    name: `radio-group${props.id}`,
    value: props.title,
    checked: props.checked,
    title: props.title,
    checkEvent: e => props.handleOptionChange(e, props.questionIndex, props.id),
  };
  return (
    <label htmlFor={`radio-selector${props.id}`} className="radio-wrapper">
      <input
        id={`radio-selector${props.id}`}
        type={data.type}
        name={data.name}
        value={data.value}
        checked={data.checked}
        onChange={data.checkEvent}
      />
      <span className="radio-text">{data.title}</span>
    </label>
  );
};

AnswerCheckBox.propTypes = {
  title: React.PropTypes.string.isRequired,
  checked: React.PropTypes.bool.isRequired,
  handleOptionChange: React.PropTypes.func.isRequired,
  id: React.PropTypes.number.isRequired,
  questionIndex: React.PropTypes.number.isRequired,
};

export default AnswerCheckBox;
