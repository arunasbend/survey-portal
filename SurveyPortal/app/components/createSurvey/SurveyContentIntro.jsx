import React from 'react';
import { Field } from 'redux-form';
import DateTimePicker from 'react-widgets/lib/DateTimePicker';
import moment from 'moment';
import momentLocaliser from 'react-widgets/lib/localizers/moment';
import 'react-widgets/dist/css/react-widgets.css';
import UserGroup from './UserGroup';

momentLocaliser(moment);

const inputField = field => (
  <div>
    <label htmlFor={field.id} >{field.label}</label>
    <input
      {...field.input}
      placeholder={field.placeholder}
      className={field.className}
      id={field.id}
      type={field.type}
    />
    {field.meta.touched && (field.meta.error && <div style={{ right: '160px' }} className="survey-form__error-tooltip"> {field.meta.error}</div>) }
  </div>
);

const renderDateTimePicker = (
  { input: { onChange, value }, showTime, meta: { touched, error } }) => (
    <div>
      <DateTimePicker
        onChange={onChange}
        format="DD MMM YYYY"
        time={showTime}
        value={!value ? null : new Date(value)}
        className="survey-form--due-date"
      />
      {touched && (error && <div style={{ right: '160px' }} className="survey-form__error-tooltip"> {error}</div>) }
    </div>
);

const SurveyContentIntro = (props) => {
  const renderUserGroups = () => (
        props.userGroups.map(userGroup => (
          <UserGroup
            key={userGroup.id}
            title={userGroup.title}
            onToggle={props.onToggle}
            id={userGroup.id}
            selected={userGroup.selected}
          />
        ))
      );
  return (
    <div className="survey-content">
      <h1 className="main-headline">{props.surveyTitle}</h1>
      <div className="survey-content__intro">
        <fieldset className="survey-form">
          <div className="survey-form__group">
            <Field
              id="sf-input-name"
              type="text"
              name="title"
              component={inputField}
              label="Title"
              placeholder="Write survey title here..."
            />
          </div>
          <div className="survey-form__group">
            <Field
              id="sf-input-name"
              type="text"
              name="description"
              component={inputField}
              label="Description"
              placeholder="Describe the survey..."
            />
          </div>
          <div className="survey-form__group clearfix">
            <Field
              name="date"
              showTime={false}
              component={renderDateTimePicker}
            />
          </div>
          <h4>User Groups:</h4>
          <div id="sf-user-groups" className="survey-form__group survey-form__group--user-groups">
            {renderUserGroups()}
          </div>
          <button type="submit" className="submitButton">Submit</button>
        </fieldset>
      </div>
    </div>);
};

renderDateTimePicker.propTypes = {
  input: React.PropTypes.shape({
    onChange: React.PropTypes.func.isRequired,
    value: React.PropTypes.date,
  }).isRequired,
  showTime: React.PropTypes.bool.isRequired,
  meta: React.PropTypes.shape({
    touched: React.PropTypes.bool,
    error: React.PropTypes.string,
    warning: React.PropTypes.bool,
  }).isRequired,

};

SurveyContentIntro.propTypes = {
  surveyTitle: React.PropTypes.string.isRequired,
  userGroups: React.PropTypes.arrayOf(React.PropTypes.shape({
    title: React.PropTypes.string,
    id: React.PropTypes.number,
    selected: React.PropTypes.bool,
  })).isRequired,
};

export default SurveyContentIntro;
