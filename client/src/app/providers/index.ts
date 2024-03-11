import compose from 'compose-function';
import { withAntdConfig } from './withAntdConfig';
import { withRouter } from './withRouter';
import { withApiSettings } from './withApiSettings';

const withProviders = compose(withAntdConfig, withRouter, withApiSettings);

export default withProviders;
