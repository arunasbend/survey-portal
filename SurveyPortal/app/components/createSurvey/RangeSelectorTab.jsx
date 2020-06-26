import React from 'react';
import { Field } from 'redux-form';

const RangeSelectorTab = (props) => {
  const renderOptions = () => {
    const optionValues = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    return (
        optionValues.map(item => (
          <option key={item} >{item}</option>
        ))
    );
  };

  return (
    <fieldset className="survey-form">
      <div className="survey-form__group clearfix">
        <div className="survey-form--input-first">
          <label htmlFor="sf-input-tab4-min">Min:</label>
          <Field id="sf-input-tab4-min" type="text" placeholder="Text" component="input" name={`questions[${props.number}].${props.type}.min`} />
        </div>
        <div className="survey-form--select-value">
          <label htmlFor="sf-input-tab4-range">Range:</label>
          <Field component="select" id="sf-input-tab4-range" name={`questions[${props.number}].${props.type}.range`} >
            {renderOptions()}
          </Field>
        </div>
        <div className="survey-form--input-last">
          <label htmlFor="sf-input-tab4-max">Max:</label>
          <Field id="sf-input-tab4-max" type="text" component="input" placeholder="Text" name={`questions[${props.number}].${props.type}.max`} />
        </div>
      </div>
    </fieldset>
  );
};

RangeSelectorTab.propTypes = {
  number: React.PropTypes.number.isRequired,
  type: React.PropTypes.string.isRequired,
};

export default RangeSelectorTab;
