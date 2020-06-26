import React from 'react';
import { Field } from 'redux-form';

const TextAreaTab = props => (
  <fieldset className="survey-form">
    <div className="survey-form__group">
      <Field name={`questions[${props.number}].${props.type}.text`} component="textarea" placeholder="Answer box placeholder" />
    </div>
  </fieldset>
);

TextAreaTab.propTypes = {
  number: React.PropTypes.number.isRequired,
  type: React.PropTypes.string.isRequired,
};

export default TextAreaTab;
