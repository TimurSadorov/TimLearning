import React from 'react';
import { FC } from 'react';
import { Api, Config, SiteLocalStorage } from '@shared';

// eslint-disable-next-line react/display-name
export const withApiSettings = (Component: FC) => () => {
    Api.Services.OpenAPI.BASE = Config.appEnv.apiUrl;
    Api.Services.OpenAPI.TOKEN = () => Promise.resolve(SiteLocalStorage.getAccessToken() ?? '');

    return <Component />;
};
