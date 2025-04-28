const check = (rules: any, action: any, data: any) => {
  const staticPermissions = rules;
  if (action) {
    if (staticPermissions && staticPermissions.includes(action)) {
      return true;
    }
  } else {
    return true;
  }
  return false;
};

const Can = (props: any) => {
  let isOK = false;
  if (props.rules) {
    isOK = check(props.rules, props.perform, props.data) ? true : false;
  }
  return isOK ? props.yes() : props.no();
};

Can.defaultProps = {
  yes: () => null,
  no: () => null,
};

export default Can;
