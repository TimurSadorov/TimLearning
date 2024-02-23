import React from 'react';
import { FC } from 'react';
import { OpenAPI } from 'shared/api';
import { appEnv } from 'shared/config';

// eslint-disable-next-line react/display-name
export const withApiUrl = (Component: FC) => () => {
    OpenAPI.BASE = appEnv.apiUrl;
    return <Component />;
};
