import React from 'react';

const Heading = (props) => {
  const description = props.heading.description;
  const dueDate = props.heading.dueDate;
  const statNumber = props.heading.totalSubmissions;
  const title = props.heading.title;
  return (
    <div>
      <h1 className="main-headline">{title}</h1>
      <div className="module-stats module-stats">
        <div className="module-stats__item">
          <div className="module-stats__head">
            <span className="fa fa-user" />Total submissions
          </div>
          <div className="module-stats__numbers">{statNumber}</div>
        </div>
      </div>
      <div className="survey-content__intro">
        <p>{description}</p>
        <div className="survey-content__intro-icons">
          <span className="fa fa-calendar fa-border fa-pull-left" />
          <span>Due date:<br />{dueDate}</span>
        </div>
      </div>
    </div>
  );
};

Heading.propTypes = {
  heading: React.PropTypes.shape({
    description: React.PropTypes.string,
    title: React.PropTypes.string,
    dueDate: React.PropTypes.string.isRequired,
    totalSubmissions: React.PropTypes.number.isRequired,
  }).isRequired,
};

export default Heading;
