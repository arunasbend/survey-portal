import React from 'react';

export const checkBoxField = field => (
  <label htmlFor={field.id} >
    <input
      {...field.input}
      placeholder={field.placeholder}
      className={field.className}
      id={field.id}
      type={field.type}
    />
    <span className="fake-box" />
    <span className="checkbox-text">{field.label}</span>
  </label>
);
