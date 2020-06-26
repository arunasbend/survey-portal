import React, { PropTypes } from 'react';

const RangeCell = props => (
  <td className="content__table--cell-headline">
    <div className="progress-bars">
      <div className="progress-bars__row">
        <div className="progress-head">
          {props.index + 1}.
        </div>
        <div className="progress-container">
          <div className="progressbar" style={{ width: `${props.amount}%` }} />
        </div>
        <div className="progress-head2">
          {props.amount}%
        </div>
      </div>
    </div>
  </td>
);

RangeCell.propTypes = {
  index: PropTypes.number.isRequired,
  amount: PropTypes.number.isRequired,
};

export default RangeCell;
