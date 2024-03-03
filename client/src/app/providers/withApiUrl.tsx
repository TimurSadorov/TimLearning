import React from 'react';
import { FC } from 'react';
import { Api, Config } from '@shared';

// eslint-disable-next-line react/display-name
export const withApiUrl = (Component: FC) => () => {
    Api.Services.OpenAPI.BASE = Config.appEnv.apiUrl;
    return <Component />;
};
