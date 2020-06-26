import React from 'react';
import shortId from 'shortid';

const LinearDiagram = (props) => {
  const diagramData = props.linearDiagram;
  const bars = diagramData.map(bar => (
    <div key={shortId.generate()} className="progress-bars__row">
      <div className="progress-head">{bar.title}</div>
      <div className="progress-container">
        <div className="progressbar" style={{ width: bar.amount }} />
      </div>
      <div className="progress-head2">{bar.amount}</div>
    </div>
  ));
  return (
    <div className="module-dashboard__item">
      <h2 className="heading2">Survey Stats One</h2>
      <h3>Survey Usage</h3>
      <div className="progress-bars">
        {bars}
      </div>
    </div>
  );
};

LinearDiagram.propTypes = {
  linearDiagram: React.PropTypes.arrayOf(React.PropTypes.shape({
    title: React.PropTypes.string,
    amount: React.PropTypes.number,
  })).isRequired,
};

export default LinearDiagram;
