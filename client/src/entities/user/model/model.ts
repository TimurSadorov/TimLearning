import { createEvent, createStore, restore } from 'effector';
import { reset } from 'patronum';
import { loadUserFx, loginFx, registerFx } from './effects';
import { restoreFail } from '@shared';
import { createGate } from 'effector-react';

export const $user = restore(loadUserFx, null);
export const $isLoging = createStore(false);
export const resetUser = createEvent();
reset({ clock: resetUser, target: [$user, $isLoging] });

export const LoginGate = createGate();
export const resetErrorOnLogin = createEvent();
export const $errorOnLogin = restoreFail(loginFx, null).reset(LoginGate.close);

export const RegistrationGate = createGate();
export const $isSuccessRegister = createStore(false).on(registerFx.done, () => true);
export const $errorOnRegistration = restoreFail(registerFx, null);
reset({ clock: RegistrationGate.close, target: [$isSuccessRegister, $errorOnRegistration] });
