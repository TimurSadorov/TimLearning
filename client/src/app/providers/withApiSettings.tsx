import React from 'react';
import { FC } from 'react';
import { Api, Config, SiteLocalStorage, Utils } from '@shared';
import { UserEntity } from '@entities';

// eslint-disable-next-line react/display-name
export const withApiSettings = (Component: FC) => () => {
    Api.Services.OpenAPI.BASE = Config.appEnv.apiUrl;
    Api.Services.OpenAPI.TOKEN = async () => {
        const token = SiteLocalStorage.getAccessToken();
        if (!token) {
            return '';
        }

        const jwtInfo = Utils.decodeSystemInfoJwt(token);
        if (jwtInfo.expire > new Date(Date.now() + 10_000)) {
            return token;
        }

        const refreshToken = SiteLocalStorage.getRefreshToken();
        if (!refreshToken) {
            UserEntity.Model.logoutFx();
            return '';
        }

        try {
            SiteLocalStorage.clearTokens();
            const response = await Api.Services.UserAccountService.refresh({
                userEmail: jwtInfo.email,
                refreshToken: refreshToken,
            });

            UserEntity.Model.setAuthTokensFx(response);
            return response.accessToken;
        } catch {
            UserEntity.Model.logoutFx();
            return '';
        }
    };

    return <Component />;
};
