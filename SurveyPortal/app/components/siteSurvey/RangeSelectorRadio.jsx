/* eslint-disable jsx-a11y/no-static-element-interactions */
import React from 'react';

const RangeSelectorRadio = (props) => {
  const toggleActive = `range-wrapper ${props.number.toString() === props.active ? 'active' : ''}`;
  return (
    <label
      htmlFor={`range-selector${props.number}`}
      className={toggleActive}
      onClick={e => props.handleRangeSelectorRadioValue(e, props.id, props.number)}
    >
      <input
        type="radio"
        name="range-selector-radio"
        value={props.number}
      />
      <span>{props.number}</span>
    </label>
  );
};

RangeSelectorRadio.propTypes = {
  number: React.PropTypes.number.isRequired,
  active: React.PropTypes.string.isRequired,
  handleRangeSelectorRadioValue: React.PropTypes.func.isRequired,
  id: React.PropTypes.number.isRequired,
};

export default RangeSelectorRadio;
