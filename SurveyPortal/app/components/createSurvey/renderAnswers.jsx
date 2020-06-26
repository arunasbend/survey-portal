/* eslint-disable react/no-array-index-key */
import React from 'react';
import { Field } from 'redux-form';
import Answer from './Answer';

const renderAnswers = ({ fields }) => {
  const addAnswer = () => {
    fields.push();
  };
  const removeAnswer = () => {
    if (fields.length === 1) {
      return;
    }
    fields.pop();
  };
  return (
    <div>
      {fields.map((answer, index) => (
        <Field
          key={index}
          id={`sf-input-tab${index}-question2`}
          component={Answer}
          label={`${index + 1}. Answer`}
          name={answer}
          addAnswer={addAnswer}
          removeAnswer={removeAnswer}
          last={fields.length - 1 === index}
        />
          ),
        )}
    </div>
  );
};

renderAnswers.propTypes = {
  fields: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
};

export default renderAnswers;
