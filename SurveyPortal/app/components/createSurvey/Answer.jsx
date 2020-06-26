import React from 'react';

const Answer = ({ addAnswer, removeAnswer, last, input, id, label,
   meta: { touched, error } }) => {
  let surveyFormClass = 'survey-form__group ';
  if (touched && error) {
    surveyFormClass += 'survey-form__error';
  }
  let addRemove = null;

  if (last) {
    addRemove = (<div className="survey-form__modify">
      <a href="#close" className="btn-modify" onClick={removeAnswer}>
        <i className="fa fa-close" />
      </a>
      <a href="#plus" className="btn-modify" onClick={addAnswer}>
        <i className="fa fa-plus" />
      </a>
    </div>);
  }
  return (
    <div className={surveyFormClass}>
      <label htmlFor={id} >{label}</label>
      <input {...input} id={id} />
      {touched && (error && <div className="survey-form__error-tooltip"> {error}</div>) }
      {addRemove}
    </div>);
};

Answer.propTypes = {
  input: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  id: React.PropTypes.string.isRequired,
  label: React.PropTypes.string.isRequired,
  meta: React.PropTypes.object.isRequired, // eslint-disable-line react/forbid-prop-types
  addAnswer: React.PropTypes.func.isRequired,
  removeAnswer: React.PropTypes.func.isRequired,
  last: React.PropTypes.bool.isRequired,
};

export default Answer;
