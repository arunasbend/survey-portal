/* eslint-disable jsx-a11y/no-static-element-interactions */
import React from 'react';
import shortId from 'shortid';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';

import ReactModal from 'react-modal';
import '../../styles/modal.css';
import actionCreators from '../../actions';

class SurveyLine extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      showModal: false,
    };
    this.renderModal = this.renderModal.bind(this);
    this.onToggleModal = this.onToggleModal.bind(this);
  }

  onToggleModal() {
    this.setState({ showModal: !this.state.showModal });
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
              type="button" onClick={() => this.props.deleteSurvey(this.props.id)}
              value="Delete" className="btn-action btn-action--bgr-red"
            />
          </div>
        </div>
      </ReactModal>
    );
  }

  render() {
    return (
      <tr key={shortId.generate()}>
        <td className="content__table--status">
          <span className={this.props.className}>{this.props.status}</span>
        </td>
        <td className="content__table--name">{this.props.name}</td>
        <td className="content__table--buttons">
          <a
            href={undefined}
            onClick={this.onToggleModal}
            className="btn-modify"
          >
            <i className="fa fa-close" />
          Delete
        </a>
          <Link to={`survey-edit/${this.props.id}`} className="btn-modify"><i className="fa fa-edit" />Edit</Link>
          <Link to={`site-survey/${this.props.id}`} className="btn-modify"><i className="fa fa-play" />Proceed</Link>
          <Link to={`survey-results/${this.props.id}`} className="btn-modify"><i className="fa fa-eye" />View Results</Link>
          {this.renderModal()}
        </td>
      </tr>
    );
  }
}

SurveyLine.propTypes = {
  className: React.PropTypes.string.isRequired,
  status: React.PropTypes.string.isRequired,
  name: React.PropTypes.string.isRequired,
  id: React.PropTypes.string.isRequired,
  deleteSurvey: React.PropTypes.func.isRequired,
};

const mapDispatchToProps = dispatch => bindActionCreators(actionCreators, dispatch);

export default connect(undefined, mapDispatchToProps)(SurveyLine);
