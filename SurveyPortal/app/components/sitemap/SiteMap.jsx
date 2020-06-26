import React from 'react';
import { Link } from 'react-router-dom';

const SiteMap = () => (
  <div className="survey-content">
    <h1 className="main-headline">Project sitemap</h1>
    <ul>
      <li><Link to="/edit">User Edit</Link></li>
    </ul>
    <hr />
    <ul>
      <li><Link to="/create-survey">Create Survey</Link></li>
      <li><Link to="/survey-overview">Survey Overview</Link></li>
      <li><Link to="user-groups">User Groups</Link></li>
    </ul>
  </div>
);

export default SiteMap;
