import { createEvent, createStore, restore } from 'effector';
import { reset } from 'patronum';
import { User } from '../types';
import { loadUserFx } from './effects';

export const $user = restore<User | null>(loadUserFx, null);
export const $isLoging = createStore(false);

export const resetUser = createEvent();

reset({ clock: resetUser, target: [$user, $isLoging] });
