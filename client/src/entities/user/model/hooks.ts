import { useGate, useUnit } from 'effector-react';
import { loginFx, registerFx, sendMailToRecoverPasswordFx } from './effects';
import {
    $errorOnLogin,
    $errorOnPasswordRecovery,
    $errorOnRegistration,
    $isSuccessPasswordRecovery,
    $isSuccessPasswordRecovery as $isSuccessRegistration,
    LoginGate,
    PasswordRecoveryGate,
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

export const usePasswordRecovery = (onSuccess = () => {}) => {
    useGate(PasswordRecoveryGate);

    const recover = (request: Api.Services.SendMailToRecoverPasswordRequest) => sendMailToRecoverPasswordFx(request);
    const recoverPending = useUnit(sendMailToRecoverPasswordFx.pending);
    const errorOnPasswordRecovery = useUnit($errorOnPasswordRecovery);
    const isSuccessPasswordRecovery = useUnit($isSuccessPasswordRecovery);

    useEffect(() => {
        if (isSuccessPasswordRecovery) {
            onSuccess();
        }
    }, [isSuccessPasswordRecovery, onSuccess]);

    return { recover, recoverPending, errorOnPasswordRecovery };
};
