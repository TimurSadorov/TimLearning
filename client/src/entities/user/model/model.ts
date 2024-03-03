import { createEvent, createStore, restore } from 'effector';
import { reset } from 'patronum';
import { loadUserFx, loginFx } from './effects';
import { restoreFail } from '@shared';
import { createGate } from 'effector-react';

export const $user = restore(loadUserFx, null);
export const $isLoging = createStore(false);
export const resetUser = createEvent();
reset({ clock: resetUser, target: [$user, $isLoging] });

export const LoginGate = createGate();
export const resetErrorOnLogin = createEvent();
export const $errorOnLogin = restoreFail(loginFx, null).reset(resetErrorOnLogin);
