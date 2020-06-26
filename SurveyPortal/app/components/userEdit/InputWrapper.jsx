import React from 'react';

const InputWrapper = props => (
  <div className="survey-form__group">
    <label htmlFor={`sf-input-${props.label}`}>{props.title}</label>
    <input
      id={`sf-input-${props.label}`}
      value={props.value}
      onChange={props.onChange}
      type={props.type}
      name={props.name}
    />
  </div>
);

InputWrapper.propTypes = {
  label: React.PropTypes.string.isRequired,
  value: React.PropTypes.string.isRequired,
  type: React.PropTypes.string.isRequired,
  name: React.PropTypes.string.isRequired,
  title: React.PropTypes.string.isRequired,
  onChange: React.PropTypes.func.isRequired,
};

export default InputWrapper;
