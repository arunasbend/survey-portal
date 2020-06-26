import React from 'react';

// import UserGroupCells from './UserGroupCells';
import RangeCell from './RangeCell';


const RangeRow = props => (
  <tr>
    <RangeCell index={props.index} amount={props.row.amount} />
    { /* TODO: When user groups are implemented, bring back this functionality
    <UserGroupCells userGroups={props.row.userGroups} />
    */ }
  </tr>
);

RangeRow.propTypes = {
  index: React.PropTypes.number.isRequired,
  row: React.PropTypes.shape({
    id: React.PropTypes.string,
    amount: React.PropTypes.number,
    userGroups: React.PropTypes.arrayOf(
      React.PropTypes.shape({
        groupName: React.PropTypes.string,
        peopleCount: React.PropTypes.number,
      }),
    ),
  }).isRequired,
};

export default RangeRow;
