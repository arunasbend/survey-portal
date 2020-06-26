import React from 'react';
import shortId from 'shortid';

const Header = (props) => {
  const selectCorrectClassName = (status) => {
    switch (status) {
      case 'Disabled':
        return 'module-stats__item module-stats__item--red module-stats__item--filter';
      case 'Published':
        return 'module-stats__item module-stats__item--green module-stats__item--filter';
      case 'Completed':
        return 'module-stats__item module-stats__item--blue module-stats__item--filter';
      default:
        return 'module-stats__item module-stats__item--navy module-stats__item--filter';
    }
  };
  const statData = props.header.statisticsData;
  const icons = ['fa fa-database', 'fa fa-bars', 'fa fa-check'];
  const statItems = statData.map((data, index) => (
    <div key={shortId.generate()} className="module-stats__item">
      <div className="module-stats__head">
        <i className={icons[index]} />{data.title}
      </div>
      <div className="module-stats__numbers">{data.count}</div>
    </div>
  ));
  const surveyItemsData = props.header.surveyData;
  const surveyItems = surveyItemsData.map(data => (
    <div key={shortId.generate()} className={selectCorrectClassName(data.title)}>
      <div className="module-stats__head">{data.title}</div>
      <div className="module-stats__numbers">{data.count}</div>
      <a href="#delete" className="filter-icon">
        <i className="fa fa-filter" aria-hidden="true" />
      </a>
    </div>
  ));
  return (
    <div className="module-stats module-stats--six-cols">
      {surveyItems}
      {statItems}
    </div>
  );
};

Header.propTypes = {
  header: React.PropTypes.shape({
    statisticsData: React.PropTypes.arrayOf(React.PropTypes.shape({
      title: React.PropTypes.string,
      icon: React.PropTypes.string,
      count: React.PropTypes.number,
    })),
    surveyData: React.PropTypes.arrayOf(React.PropTypes.shape({
      title: React.PropTypes.string,
      class: React.PropTypes.string,
      count: React.PropTypes.number,
    })),
  }).isRequired,
};

export default Header;
