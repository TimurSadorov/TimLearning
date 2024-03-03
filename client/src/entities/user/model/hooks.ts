import { useGate, useUnit } from 'effector-react';
import { loginFx, registerFx } from './effects';
import {
    $errorOnLogin,
    $errorOnRegistration,
    $isSuccessRegister as $isSuccessRegistration,
    LoginGate,
    RegistrationGate,
} from './model';
import { Api } from '@shared';
import { useEffect } from 'react';

export const useLogin = () => {
    useGate(LoginGate);

    const login = (request: Api.Services.LoginRequest) => loginFx(request);
    const loginPending = useUnit(loginFx.pending);
    const errorOnLogin = useUnit($errorOnLogin);

    return { login, loginPending, errorOnLogin };
};

export const useRegistration = (onSuccess = () => {}) => {
    useGate(RegistrationGate);

    const register = (request: Api.Services.NewUserRequest) => registerFx(request);
    const registrationPending = useUnit(registerFx.pending);
    const errorOnRegistration = useUnit($errorOnRegistration);
    const isSuccessRegistration = useUnit($isSuccessRegistration);

    useEffect(() => {
        if (isSuccessRegistration) {
            onSuccess();
        }
    }, [isSuccessRegistration, onSuccess]);

    return { register, registrationPending, errorOnRegistration };
};
