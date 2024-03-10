import { sample } from 'effector';
import { loadUserFx, loginFx, logoutFx } from './effects';
import { $isLoging } from './model';

sample({
    clock: loadUserFx.finally,
    fn: () => true,
    target: $isLoging,
});

sample({
    clock: loginFx.doneData,
    target: loadUserFx,
});

sample({
    clock: logoutFx.doneData,
    target: loadUserFx,
});
