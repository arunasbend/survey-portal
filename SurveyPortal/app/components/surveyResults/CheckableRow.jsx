import React, { PropTypes } from 'react';

import DoughnutChart from '../charts/DoughnutChart';
// import UserGroupCells from './UserGroupCells';
import CheckableCell from './CheckableCell';

const CheckablesRow = (props) => {
  const percentages = props.rows.map(row => (
    {
      amount: row.amount,
      id: row.id,
    }
  ));
  let chart = props.index === 0 &&
    <td rowSpan={percentages.length}>
      <DoughnutChart percentages={percentages} />
    </td>;
  if (props.type === 'check') {
    chart = null;
  }
  return (
    <tr>
      <CheckableCell row={props.row} type={props.type} />
      <td
        className="content__table--result-numbers
        content__table--border-right content__table--border-left"
      >
        {props.row.amount}%
      </td>
      { /* TODO: When user groups are implemented, bring back this functionality
      <UserGroupCells userGroups={props.row.userGroups} />
      */ }
      {chart}
    </tr>
  );
};

CheckablesRow.propTypes = {
  rows: PropTypes.arrayOf(React.PropTypes.shape({
    row: PropTypes.shape({
      amount: PropTypes.number,
      label: PropTypes.string,
      checked: PropTypes.bool,
      id: PropTypes.number,
      userGroups: PropTypes.arrayOf(
        PropTypes.shape({
          groupName: PropTypes.string,
          peopleCount: PropTypes.number,
        }),
      ),
    }) })).isRequired,
  index: PropTypes.number.isRequired,
  type: PropTypes.string.isRequired,
  row: PropTypes.shape({
    amount: PropTypes.number,
    label: PropTypes.string,
    checked: PropTypes.bool,
    userGroups: PropTypes.arrayOf(
      PropTypes.shape({
        groupName: PropTypes.string,
        peopleCount: PropTypes.number,
      }),
    ),
  }).isRequired,
};

export default CheckablesRow;
