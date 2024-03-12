import { createEffect } from 'effector';
import { SiteLocalStorage, Api, Utils } from '@shared';

export const loadUserFx = createEffect(() => {
    const token = SiteLocalStorage.getAccessToken();
    const user = token ? Utils.decodeUserJwt(token) : null;

    return user;
});

export const loginFx = createEffect(async (data: Api.Services.LoginRequest) => {
    return await Api.Services.UserAccountService.login(data);
});

export const setAuthTokensFx = createEffect((tokens: Api.Services.AuthTokensResponse) => {
    SiteLocalStorage.setAccessToken(tokens.accessToken);
    SiteLocalStorage.setRefresfToken(tokens.refreshToken);
});

export const logoutFx = createEffect(() => {
    SiteLocalStorage.clearTokens();
});

export const registerFx = createEffect(async (data: Api.Services.NewUserRequest) => {
    await Api.Services.UserAccountService.register(data);
});

export const sendMailToRecoverPasswordFx = createEffect(async (data: Api.Services.SendMailToRecoverPasswordRequest) => {
    await Api.Services.UserAccountService.sendMailToRecoverPassword(data);
});

export const recoverPasswordFx = createEffect(async (data: Api.Services.RecoverPasswordRequest) => {
    await Api.Services.UserAccountService.recoverPassword(data);
});

export const confirmEmailFx = createEffect(async (data: Api.Services.UserEmailConfirmationRequest) => {
    await Api.Services.UserAccountService.confirmEmail(data);
});
