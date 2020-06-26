import React from 'react';

const TextArea = props => (
  <fieldset className="survey-form">
    <div className="survey-form__group">
      <textarea
        id={props.id}
        placeholder={props.value}
        onInput={e => props.handleTextAreaContentChange(e, props.id)}
      />
    </div>
  </fieldset>
);

TextArea.propTypes = {
  handleTextAreaContentChange: React.PropTypes.func.isRequired,
  value: React.PropTypes.string.isRequired,
  id: React.PropTypes.number.isRequired,
};

export default TextArea;
