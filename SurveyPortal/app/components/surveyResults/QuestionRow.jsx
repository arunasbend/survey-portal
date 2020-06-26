import React, { PropTypes } from 'react';
import CheckablesRow from './CheckableRow';
import RangeRow from './RangeRow';
import TextRow from './TextRow';

const QuestionRow = (props) => {
  switch (props.type) {
    case 'radio':
    case 'check':
      return (
        <CheckablesRow
          rows={props.rows} row={props.rows[props.index]}
          type={props.type} index={props.index}
        />
      );
    case 'range':
      return <RangeRow row={props.rows[props.index]} index={props.index} />;
    case 'text':
      return <TextRow row={props.rows[props.index]} />;
    default:
      return <tr />;
  }
};

QuestionRow.propTypes = {
  type: PropTypes.string.isRequired,
  rows: PropTypes.arrayOf(PropTypes.object).isRequired,
  index: PropTypes.number.isRequired,
};

export default QuestionRow;
