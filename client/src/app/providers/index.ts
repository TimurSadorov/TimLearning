import compose from 'compose-function';
import { withAntdConfig } from './withAntdConfig';
import { withRouter } from './withRouter';
import { withApiUrl } from './withApiUrl';

const withProviders = compose(withAntdConfig, withRouter, withApiUrl);

export default withProviders;
