import React, { PropTypes } from 'react';
import User from './../../resources/images/usr.jpg';

const TextRow = props => (
  <tr className="survey-results">
    <td className="survey-results__user-info">
      <figure className="survey-results__user-pic">
        <a href={undefined}><img alt="User: " src={User} /></a>
      </figure>
      <a href={undefined} className="survey-results__username">
        {props.row.userName}
      </a>
    </td>
    <td className="survey-results__user-textarea">
      {props.row.textArea}
    </td>
  </tr>
    )
;

TextRow.propTypes = {
  row: PropTypes.shape({
    imageUrl: PropTypes.string,
    userName: PropTypes.string,
    textArea: PropTypes.string,
  }).isRequired,
};

export default TextRow;
