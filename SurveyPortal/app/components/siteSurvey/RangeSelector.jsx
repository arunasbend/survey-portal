import React from 'react';

import RangeSelectorRadio from './RangeSelectorRadio';

const renderRangeSelectorRadio = (data) => {
  const ratingValues = Array.from({ length: data.size }, (v, k) => k + 1);
  return (
  ratingValues.map(item => (
    <RangeSelectorRadio
      number={item}
      key={item}
      handleRangeSelectorRadioValue={data.handleRangeSelectorRadioValue}
      active={data.active}
      id={data.id}
    />),
    )
  );
};


const RangeSelector = (props) => {
  const data = {
    handleRangeSelectorRadioValue: props.handleRangeSelectorRadioValue,
    active: props.active,
    id: props.id,
    lowerLimitText: props.data[0],
    size: Number(props.data[1]),
    upperLimitText: props.data[2],
  };
  return (
    <fieldset className="survey-form">
      <div className="survey-form__group--wide  clearfix">
        <div className="survey-form--input-first-text">{data.lowerLimitText}</div>
        <div id="radio-range-selector" className="survey-form__range-select">
          {renderRangeSelectorRadio(data)}
        </div>
        <div className="survey-form--input-last-text">{data.upperLimitText}</div>
      </div>
    </fieldset>
  );
};

RangeSelector.propTypes = {
  handleRangeSelectorRadioValue: React.PropTypes.func.isRequired,
  active: React.PropTypes.string.isRequired,
  id: React.PropTypes.number.isRequired,
  data: React.PropTypes.array.isRequired, // eslint-disable-line react/forbid-prop-types
};

export default RangeSelector;
