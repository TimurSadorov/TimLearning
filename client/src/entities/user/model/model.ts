import { restore } from 'effector';
import { createGate } from 'effector-react';
import { User } from '../types';
import { loadUserFx } from './effects';

export const UserGate = createGate();

export const $user = restore<User | null>(loadUserFx, null);
export const $userLoading = loadUserFx.pending;
