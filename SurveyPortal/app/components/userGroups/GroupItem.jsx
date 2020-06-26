import React from 'react';

class GroupItem extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      editEnabled: false,
      editableTitle: this.props.title,
    });

    this.onToggleTaskEdit = this.onToggleTaskEdit.bind(this);
    this.onRename = this.onRename.bind(this);
    this.onHandleChange = this.onHandleChange.bind(this);
    this.renderTaskTitleEdit = this.renderTaskTitleEdit.bind(this);
  }

  onToggleTaskEdit() {
    this.setState({
      editEnabled: !this.state.editEnabled,
    });
  }

  onRename(e) {
    e.preventDefault();
    this.props.onRename(this.props.id, this.state.editableTitle);
    this.setState({
      editEnabled: false,
    });
  }

  onHandleChange(e) {
    this.setState({
      editableTitle: e.target.value,
    });
  }

  renderTaskTitleEdit() {
    return (
      <form onSubmit={this.onRename}>
        <input
          type="text"
          value={this.state.editableTitle}
          onChange={this.onHandleChange}
        />
      </form>
    );
  }

  render() {
    return (
      <tr>
        <td className="user-groups__name">
          <div>
            <div>
              <a href="#group">
                {this.state.editEnabled ? this.renderTaskTitleEdit() : this.state.editableTitle}
              </a>
            </div>
          </div>
        </td>
        <td>{this.props.members}</td>
        <td>{this.props.surveys}</td>
        <td className="user-groups__buttons">
          <a
            href="#delete"
            className="btn-modify"
            onClick={() => this.props.removeGroup(this.props.id)}
          >
            <i className="fa fa-close" />Delete</a>
          <a
            href="#edit"
            className="btn-modify"
            onClick={this.onToggleTaskEdit}
          >
            <i className="fa fa-edit" />Edit
          </a>
        </td>
      </tr>
    );
  }
}

GroupItem.propTypes = {
  members: React.PropTypes.number.isRequired,
  title: React.PropTypes.string.isRequired,
  removeGroup: React.PropTypes.func.isRequired,
  surveys: React.PropTypes.number.isRequired,
  id: React.PropTypes.number.isRequired,
  onRename: React.PropTypes.func.isRequired,
};

export default GroupItem;
