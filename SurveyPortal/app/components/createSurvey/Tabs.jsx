import React from 'react';
import TabTitle from './TabTitle';

class Tabs extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedTab: 1,
      titles: ['check', 'radio', 'text', 'range'],
    };
    this.renderTabTitles = this.renderTabTitles.bind(this);
    this.onSelectingTab = this.onSelectingTab.bind(this);
  }

  onSelectingTab(e) {
    const selectedTab = parseInt(e.target.title, 10);
    this.setState({
      selectedTab,
    });
  }

  generateTabTitles() {
    return (
      this.state.titles.map((title, index) => (
        <TabTitle
          key={`${this.props.id}${title}`}
          number={this.props.number}
          index={index}
          selectedTab={this.state.selectedTab}
          onSelectingTab={this.onSelectingTab}
          value={title}
        />),
      )
    );
  }

  renderTabTitles() {
    return (
      <ul className="tabs__nav pointer">
        {this.generateTabTitles()}
      </ul>
    );
  }

  render() {
    return (
      <div className="tabs">
        {this.renderTabTitles()}
        {this.props.children[this.state.selectedTab - 1]}
      </div>
    );
  }
}

Tabs.propTypes = {
  children: React.PropTypes.arrayOf(React.PropTypes.element).isRequired,
  id: React.PropTypes.number.isRequired,
  number: React.PropTypes.number.isRequired,
};

export default Tabs;
