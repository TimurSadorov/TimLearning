import { useGate, useUnit } from 'effector-react';
import { confirmEmailFx, loginFx, recoverPasswordFx, registerFx, sendMailToRecoverPasswordFx } from './effects';
import {
    $errorOnEmailConfirmation,
    $errorOnLogin,
    $errorOnPasswordRecovery,
    $errorOnRecoveryPasswordChanging,
    $errorOnRegistration,
    $isSuccessEmailConfirmation,
    $isSuccessPasswordRecovery,
    $isSuccessRecoveryPasswordChanging,
    $isSuccessRegistration,
    EmailConfirmationGate,
    LoginGate,
    PasswordRecoveryGate,
    RecoveryPasswordChangingGate,
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

export const useRecoveryPasswordChanging = (onSuccess = () => {}) => {
    useGate(RecoveryPasswordChangingGate);

    const change = (request: Api.Services.RecoverPasswordRequest) => recoverPasswordFx(request);
    const changePending = useUnit(recoverPasswordFx.pending);
    const errorOnRecoveryPasswordChanging = useUnit($errorOnRecoveryPasswordChanging);
    const isSuccessRecoveryPasswordChanging = useUnit($isSuccessRecoveryPasswordChanging);

    useEffect(() => {
        if (isSuccessRecoveryPasswordChanging) {
            onSuccess();
        }
    }, [isSuccessRecoveryPasswordChanging, onSuccess]);

    return { change, changePending, errorOnRecoveryPasswordChanging };
};

export const useEmailConfirmation = (onSuccess = () => {}, onFail: (error: Error) => void = () => {}) => {
    useGate(EmailConfirmationGate);

    const confirm = (request: Api.Services.UserEmailConfirmationRequest) => confirmEmailFx(request);
    const errorOnEmailConfirmation = useUnit($errorOnEmailConfirmation);
    const isSuccessEmailConfirmation = useUnit($isSuccessEmailConfirmation);

    useEffect(() => {
        if (isSuccessEmailConfirmation) {
            onSuccess();
        }
        if (!!errorOnEmailConfirmation) {
            onFail(errorOnEmailConfirmation);
        }
    }, [isSuccessEmailConfirmation, errorOnEmailConfirmation, onSuccess, onFail]);

    return { confirm, errorOnEmailConfirmation };
};
