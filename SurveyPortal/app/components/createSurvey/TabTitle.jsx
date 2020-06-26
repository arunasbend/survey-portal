import React from 'react';
import { Field } from 'redux-form';

const TabTitle = (props) => {
  const tabClassName = `tab${props.index + 1} ${props.index + 1 === props.selectedTab ? ' active' : ''}`;
  const invisibleBoxShadow = { boxShadow: 'none' };
  return (
    <li>
      <Field
        type="radio"
        component="input"
        style={invisibleBoxShadow}
        onClick={props.onSelectingTab}
        className={tabClassName}
        title={props.index + 1}
        name={`questions[${props.number}].type`}
        value={props.value}
      />
    </li>);
};

TabTitle.propTypes = {
  number: React.PropTypes.number.isRequired,
  index: React.PropTypes.number.isRequired,
  selectedTab: React.PropTypes.number.isRequired,
  onSelectingTab: React.PropTypes.func.isRequired,
  value: React.PropTypes.string.isRequired,
};

export default TabTitle;
