import { createEvent, createStore, restore } from 'effector';
import { reset } from 'patronum';
import {
    confirmEmailFx,
    loadUserFx,
    loginFx,
    recoverPasswordFx,
    registerFx,
    sendMailToRecoverPasswordFx,
} from './effects';
import { restoreFail } from '@shared';
import { createGate } from 'effector-react';

export const $user = restore(loadUserFx, null);
export const $isLoging = createStore(false);
export const resetUser = createEvent();
reset({ clock: resetUser, target: [$user, $isLoging] });

export const LoginGate = createGate();
export const resetErrorOnLogin = createEvent();
export const $errorOnLogin = restoreFail(null, loginFx).reset(LoginGate.close);

export const RegistrationGate = createGate();
export const $isSuccessRegistration = createStore(false).on(registerFx.done, () => true);
export const $errorOnRegistration = restoreFail(null, registerFx);
reset({ clock: RegistrationGate.close, target: [$isSuccessRegistration, $errorOnRegistration] });

export const PasswordRecoveryGate = createGate();
export const $isSuccessPasswordRecovery = createStore(false).on(sendMailToRecoverPasswordFx.done, () => true);
export const $errorOnPasswordRecovery = restoreFail(null, sendMailToRecoverPasswordFx);
reset({ clock: PasswordRecoveryGate.close, target: [$isSuccessPasswordRecovery, $errorOnPasswordRecovery] });

export const RecoveryPasswordChangingGate = createGate();
export const $isSuccessRecoveryPasswordChanging = createStore(false).on(recoverPasswordFx.done, () => true);
export const $errorOnRecoveryPasswordChanging = restoreFail(null, recoverPasswordFx);
reset({
    clock: RecoveryPasswordChangingGate.close,
    target: [$isSuccessRecoveryPasswordChanging, $errorOnRecoveryPasswordChanging],
});

export const EmailConfirmationGate = createGate();
export const $isSuccessEmailConfirmation = createStore(false).on(confirmEmailFx.done, () => true);
export const $errorOnEmailConfirmation = restoreFail(null, confirmEmailFx);
reset({
    clock: EmailConfirmationGate.close,
    target: [$isSuccessEmailConfirmation, $errorOnEmailConfirmation],
});
