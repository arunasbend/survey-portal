import React from 'react';
import shortId from 'shortid';
import QuestionRow from './QuestionRow';

const Questions = props => (
  <div>
    {
      props.questions.map(question => (
        <div className="take-survey" key={shortId.generate()}>
          <div className="take-survey__head">
            {question.title}
          </div>
          <div className="take-survey__content">
            <table className="page-content__table">
              <tbody>
                {
                  question.rows.map((row, index) =>
                    <QuestionRow
                      type={question.type}
                      rows={question.rows}
                      row={row}
                      index={index}
                      key={shortId.generate()}
                    />,
                  )
                }
              </tbody>
            </table>
          </div>
        </div>
      ))
    }
  </div>
);

Questions.propTypes = {
  questions: React.PropTypes.arrayOf(React.PropTypes.object).isRequired,
};

export default Questions;
