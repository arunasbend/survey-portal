export default (values) => {
  const errors = {};
  if (!values.username) {
    errors.username = 'Required';
  }
  if (!values.email) {
    errors.email = 'Required';
  } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
    errors.email = 'Invalid email address';
  }
  if (!values.firstname) {
    errors.firstname = 'Required';
  }
  if (!values.lastname) {
    errors.firstname = 'Required';
  }
  if (!values.password) {
    errors.password = 'Required';
  } else if (values.password.length < 6) {
    errors.password = 'Password is too short';
  }
  if (!values.confirmPassword) {
    errors.confirmPassword = 'Required';
  } else if (values.confirmPassword.length < 6) {
    errors.confirmPassword = 'Password is too short';
  } else if (values.password !== values.confirmPassword) {
    errors.confirmPassword = 'Password doesn\'t match';
  }

  return errors;
};
