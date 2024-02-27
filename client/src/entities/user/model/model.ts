import { restore } from 'effector';
import { User } from '../types';
import { effects } from './effects';

export const $user = restore<User | null>(effects.loadUserFx, null);
