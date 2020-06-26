import React from 'react';
import ReactModal from 'react-modal';
import { Field, FieldArray } from 'redux-form';

import Tabs from './Tabs';
import renderAnswers from './renderAnswers';
import TextAreaTab from './TextAreaTab';
import RangeSelectorTab from './RangeSelectorTab';

import '../../styles/modal.css';
import '../../styles/question.css';

class Question extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      displayStatus: { display: 'none' },
      showModal: false,
    };

    this.onToggleQuesion = this.onToggleQuesion.bind(this);
    this.onToggleModal = this.onToggleModal.bind(this);
    this.onRemoveQuestion = this.onRemoveQuestion.bind(this);
  }

  onToggleQuesion() {
    const displayStatus = this.state.displayStatus.display === 'none' ? 'block' : 'none';
    this.setState({
      displayStatus: { display: displayStatus },
    });
  }

  onRemoveQuestion() {
    this.onToggleModal();
    this.onToggleQuesion();
    this.props.onRemoveQuestion(this.props.number);
  }

  onToggleModal() {
    this.setState({ showModal: !this.state.showModal });
  }

  renderTabs() {
    return (
      <Tabs id={this.props.id} number={this.props.number} type="check" >
        <div className="tabs__box" id="tab1">
          <fieldset className="survey-form">
            <FieldArray name={`questions[${this.props.number}].check`} component={renderAnswers} />
          </fieldset>
        </div>
        <div className="tabs__box" id="tab2">
          <fieldset className="survey-form">
            <FieldArray name={`questions[${this.props.number}].radio`} component={renderAnswers} />
          </fieldset>
        </div>
        <div className="tabs__box" id="tab3">
          <TextAreaTab
            type="textarea"
            number={this.props.number}
            id={this.props.id}
          />
        </div>
        <div className="tabs__box" id="tab4">
          <RangeSelectorTab
            type="range"
            number={this.props.number}
            id={this.props.id}
          />
        </div>
      </Tabs>
    );
  }

  renderForm() {
    return (
      <div className="survey-form">
        <div className="survey-form__group">
          <label htmlFor={`sf-input-question${this.props.id}`}>Input Question</label>
          <Field
            id={`sf-input-question${this.props.id}`}
            type="text"
            name={`[${this.props.question}].title`}
            component="input"
          />
        </div>
        <div className="survey-form__group">
          <label htmlFor={`sf-input-description${this.props.id}`} >Description</label>
          <Field
            id={`sf-input-description${this.props.id}`}
            name={`[${this.props.question}].description`}
            component="textarea"
          />
        </div>
        <div className="survey-form__group">
          <div className="checkbox-wrapper">
            <div className="checkbox-wrapper">
              <label htmlFor={`${this.props.id}checkbox`}>
                <Field id={`${this.props.id}checkbox`} name={`questions[${this.props.number}].checked`} component="input" type="checkbox" />
                <span className="fake-box" />
                <span className="checkbox-text"> mandatory</span>
              </label>
            </div>
          </div>
        </div>
      </div>
    );
  }

  renderModal() {
    return (
      <ReactModal
        isOpen={this.state.showModal}
        contentLabel="Minimal Modal Example"
        className="modal-content-my"
        overlayClassName="my-modal"
        parentSelector={() => document.querySelector('body')}
      >
        <h1>Are you sure you want to delete this question?</h1>
        <div className="survey-bottom-nav">
          <div className="survey-bottom-nav--left">
            <input
              type="button" onClick={this.onToggleModal}
              value="No" className="btn-action js-close-modal"
            />
          </div>
          <div className="survey-bottom-nav--right">
            <input
              type="button" onClick={this.onRemoveQuestion}
              value="Delete" className="btn-action btn-action--bgr-red"
            />
          </div>
        </div>
      </ReactModal>
    );
  }

  render() {
    return (
      <div className="survey-wrap">
        <a
          href={`#${this.props.number + 1}`}
          role="button"
          onClick={this.onToggleQuesion}
          className="survey-wrap__head survey-wrap__head--movable"
        >
          <span className="survey-wrap__head--movable--icon" />
          <span className="questionTitle">Question {this.props.number + 1}</span>
        </a>
        <div className="survey-wrap__content clearfix" style={this.state.displayStatus}>
          {this.renderForm()}
          <h4 className="heading4">Answers</h4>
          {this.renderTabs()}
          <input
            type="button" value="Remove" onClick={this.onToggleModal}
            className="btn-action btn-action--bgr-gray
            btn-action--position-right btn-action--margin-top js-open-modal"
          />
          {this.renderModal()}
        </div>
      </div>
    );
  }
}

Question.propTypes = {
  number: React.PropTypes.number.isRequired,
  onRemoveQuestion: React.PropTypes.func.isRequired,
  id: React.PropTypes.number.isRequired,
  question: React.PropTypes.string.isRequired,
};

export default Question;
