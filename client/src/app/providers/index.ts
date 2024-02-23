import compose from 'compose-function';
import { withAntdConfig } from './with-antd-config';
import { withRouter } from './with-router';
import { withApiUrl } from './with-api-url';

const withProviders = compose(withAntdConfig, withRouter, withApiUrl);

export default withProviders;
