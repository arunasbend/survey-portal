import React from 'react';

const CheckableCell = (props) => {
  const label = props.row.label;
  const wrapperClass = props.type === 'radio' ? 'radio-wrapper' : 'checkbox-wrapper';
  const inputType = props.type === 'radio' ? 'radio' : 'checkbox';
  return (
    <td className="content__table--cell-headline">
      <div className={wrapperClass}>
        <input type={inputType} value="check-1" disabled />
        {label}
      </div>
    </td>
  );
};

CheckableCell.propTypes = {
  type: React.PropTypes.string.isRequired,
  row: React.PropTypes.shape({
    label: React.PropTypes.string,
    checked: React.PropTypes.bool,
    id: React.PropTypes.string,
  }).isRequired,
};

export default CheckableCell;
