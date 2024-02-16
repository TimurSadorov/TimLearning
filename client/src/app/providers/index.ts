import { withAntdConfig } from './with-antd-config';
import { withRouter } from './with-router';
import compose from 'compose-function';

const withProviders = compose(withAntdConfig, withRouter);

export default withProviders;
