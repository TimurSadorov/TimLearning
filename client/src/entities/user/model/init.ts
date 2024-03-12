import { sample } from 'effector';
import { loadUserFx, loginFx, logoutFx, setAuthTokensFx } from './effects';
import { $isLoging } from './model';

sample({
    clock: loadUserFx.finally,
    fn: () => true,
    target: $isLoging,
});

sample({
    clock: loginFx.doneData,
    target: setAuthTokensFx,
});

sample({
    clock: setAuthTokensFx.doneData,
    target: loadUserFx,
});

sample({
    clock: logoutFx.doneData,
    target: loadUserFx,
});
