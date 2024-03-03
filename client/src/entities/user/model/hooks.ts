import { useGate, useUnit } from 'effector-react';
import { loginFx } from './effects';
import { $errorOnLogin, LoginGate } from './model';
import { Api } from '@shared';

export const useLogin = () => {
    useGate(LoginGate);

    const login = (request: Api.Services.LoginRequest) => loginFx(request);
    const isLogin = useUnit(loginFx.pending);
    const errorOnLogin = useUnit($errorOnLogin);

    return { login, isLogin, errorOnLogin };
};
