import React from 'react';

const DoughnutChart = (props) => {
  const colors = ['#E25668', '#5668E2', '#68E256',
    '#E28956', '#8A56E2', '#56E289',
    '#E2CF56', '#CF56E2', '#56E2CF'];
  let percentageFilled = 0;
  const whiteSpace = 0.2;

  const circles = [];
  props.percentages.forEach((percentage, i) => {
    circles.push(<circle
      key={`outer${percentage.id}`} cx="21" cy="21" r="15.9154943" fill="transparent" strokeWidth="10"
      stroke={colors[i]}
      strokeDasharray={`${percentage.amount - whiteSpace} ${(100 - percentage.amount) + whiteSpace}`}
      strokeDashoffset={100 - percentageFilled}
    />);
    percentageFilled += percentage.amount - whiteSpace;

    circles.push(<circle
      key={`inner${percentage.id}`} cx="21" cy="21" r="15.9154943"
      fill="transparent" strokeWidth="10" stroke="white"
      strokeDasharray={`${whiteSpace} ${100 - whiteSpace}`}
      strokeDashoffset={100 - percentageFilled}
    />);
    percentageFilled += whiteSpace;
  });

  return (
    <svg width="100%" height="100%" viewBox="0 0 42 42">
      <circle cx="21" cy="21" r="15.9154943" fill="white" />
      <circle cx="21" cy="21" r="15.9154943" fill="transparent" strokeWidth="10" stroke="white" />

      {circles}
    </svg>
  );
};

DoughnutChart.propTypes = {
  percentages: React.PropTypes.arrayOf(React.PropTypes.shape({
    amount: React.PropTypes.number.isRequired,
    id: React.PropTypes.string.isRequired,
  })).isRequired,
};

export default DoughnutChart;
