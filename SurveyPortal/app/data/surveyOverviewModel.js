const survey = {
  header: {
    statisticsData: [
        { title: 'In database', count: 0 },
        { title: 'Are Done', count: 0 },
        { title: 'Checked', count: 0 },
    ],
    surveyData: [
      {
        title: 'Published',
        count: 0,
      },
      {
        title: 'Disabled',
        count: 0,
      },
      {
        title: 'Completed',
        count: 0,
      },
    ],
  },
  surveyManager: [
  ],
  linearDiagram: [
    { title: 'Apple', amount: 150 },
    { title: 'Banana', amount: 120 },
    { title: 'Orange', amount: 90 },
    { title: 'Grapes', amount: 60 },
    { title: 'Kiwi', amount: 30 },
  ],
  circleDiagram: [
    { title: 'title1', amount: 10, id: 1, color: '#E25668' },
    { title: 'title2', amount: 20, id: 2, color: '#5668E2' },
    { title: 'title3', amount: 20, id: 3, color: '#68E256' },
    { title: 'title4', amount: 10, id: 4, color: '#E28956' },
    { title: 'title5', amount: 40, id: 5, color: '#8A56E2' },
  ],
};

export default survey;
