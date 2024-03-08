import { createEffect } from 'effector';
import { jwtDecode } from 'jwt-decode';
import { SiteLocalStorage, Api } from '@shared';
import { User } from '../types';

export const loadUserFx = createEffect(() => {
    const token = SiteLocalStorage.getAccessToken();
    const user = token ? jwtDecode<User>(token) : null;

    return user;
});

export const loginFx = createEffect(async (data: Api.Services.LoginRequest) => {
    const tokens = await Api.Services.UserAccountService.login(data);

    SiteLocalStorage.setAccessToken(tokens.accessToken);
    SiteLocalStorage.setRefresfToken(tokens.refreshToken);
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
