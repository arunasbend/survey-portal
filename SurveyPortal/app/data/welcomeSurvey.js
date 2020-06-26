export default
{
  id: '1E4C7F6E-E2F1-4461-B3B6-78963D5C833B',
  title: 'Welcome survey',
  date: '2017-04-05',
  description: 'This is a description for Welcome survey.',
  questions: [
    {
      id: 0,
      title: 'Are you an indoor or outdoor person?',
      type: 'radio',
      data: [
        {
          checked: false,
          label: 'Indoor',
        },
        {
          checked: false,
          label: 'Outdoor',
        },
      ],
    },
    {
      id: 1,
      title: 'How much do you like yourself?',
      type: 'range',
      data: '5',
    },
    {
      id: 2,
      title: 'If you were invisible for a day, what would you do?',
      type: 'text',
      data: '',
    },
    {
      id: 3,
      title: 'What do you usually eat in the morning?',
      type: 'check',
      data: [
        {
          checked: false,
          label: 'Sandwich',
        },
        {
          checked: false,
          label: 'Cereal',
        },
        {
          checked: false,
          label: 'Cookies',
        },
      ],
    },
    {
      id: 4,
      title: 'Where do you currently live?',
      type: 'text',
      data: '',
    },
  ],
};
