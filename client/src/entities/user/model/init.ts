import { sample } from 'effector';
import { loadUserFx, loginFx } from './effects';
import { $isLoging, LoginGate, resetErrorOnLogin } from './model';

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
    clock: LoginGate.close,
    target: resetErrorOnLogin,
});
