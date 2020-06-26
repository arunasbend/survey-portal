import React from 'react';
import shortId from 'shortid';
import DoughnutChart from '../charts/DoughnutChart';

const CircleDiagram = (props) => {
  const chartData = props.circleDiagram;
  const chartStatistics = chartData.map(chartStatistic => (
    <tr key={shortId.generate()}>
      <td>
        <i className="fa fa-square" style={{ color: chartStatistic.color }} />
        {chartStatistic.title}
      </td>
      <td>{chartStatistic.amount}%</td>
    </tr>
  ));
  const percentages = chartData.map(percentage => (
    {
      amount: percentage.amount,
      id: percentage.id,
    }
  ));
  return (
    <div className="module-dashboard__item">
      <h2 className="heading2">Survey Stats Two</h2>
      <h3>Top 5 Surveys</h3>
      <div className="graph-stats">
        <div className="graph-stats__row">
          <div className="graph-stats__chart">
            <DoughnutChart percentages={percentages} />
          </div>
          <div className="graph-stats__chart-info">
            <table className="graph-stats__chart-table">
              <tbody>
                {chartStatistics}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
};

CircleDiagram.propTypes = {
  circleDiagram: React.PropTypes.arrayOf(React.PropTypes.shape({
    title: React.PropTypes.string,
    amount: React.PropTypes.number,
    id: React.PropTypes.number,
    color: React.PropTypes.string,
  })).isRequired,
};

export default CircleDiagram;
